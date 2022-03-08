using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Models.Spot.BSwap;
using BLL.Services.Swaps;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace BLL.Services.SwapPools;

public class SwapPoolsService : ISwapPoolsService
{
    private readonly IMemoryCache _cache;
    private readonly IBinanceClient _client;
    private readonly ILogger<SwapPoolsService> _logger;

    public static readonly string CacheKey = nameof( SwapPoolsService );
    public static readonly string BaseCoin = "USDT";

    public SwapPoolsService( IMemoryCache cache, IBinanceClient client, ILogger<SwapPoolsService> logger )
    {
        _cache  = cache;
        _client = client;
        _logger = logger;
    }

    private async Task<IEnumerable<BinanceBSwapPool>> GetCachedPools()
    {
        IEnumerable<BinanceBSwapPool> pools = new List<BinanceBSwapPool>();

        // Try fetching pools
        if ( !_cache.TryGetValue( CacheKey, out pools ) )
        {
            // If pools are not present, try to fetch them
            await FetchSwapPools();
            _cache.TryGetValue( CacheKey, out pools );
        }

        return pools;
    }

    private async Task FetchSwapPools()
    {
        // Fetch all available coins the account can interact with
        var swapPoolsResponse = await _client.SpotApi.ExchangeData.GetLiquidityPoolsAsync();

        // Check if the response was a success
        if ( swapPoolsResponse.Success )
        {
            // We can cache the response, and make it expire in 5mns
            using var poolsEntry = _cache.CreateEntry( CacheKey );

            // We only want to fetch coins from USDT swap pools
            var availableCoinsFromBaseCoin = swapPoolsResponse.Data;

            poolsEntry.Value                           = availableCoinsFromBaseCoin;
            poolsEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes( 5 );

            // Log that cache was refreshed
            _logger.LogDebug( "Swap pools cache was set/refreshed" );
        }
        else
        {
            _logger.LogError( "Unable to fetch swap pools." );
        }
    }

    public async Task<Result<IEnumerable<BinanceBSwapPool>>> GetAll()
    {
        return Result.Ok( await GetCachedPools() );
    }

    public async Task<Result> Exists( string primaryAsset, string secondaryAsset )
    {
        try
        {
            var pools = await GetCachedPools();

            return pools.Any( p => p.Assets.Contains( primaryAsset ) && p.Assets.Contains( secondaryAsset ) )
                ? Result.Ok()
                : Result.Fail( "Does not exists" );
        }
        catch ( Exception e )
        {
            return Result.Fail( "Unable to fetch swap pools" );
        }

        
    }
}