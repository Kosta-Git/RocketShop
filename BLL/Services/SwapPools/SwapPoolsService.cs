using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
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

    private async Task<IEnumerable<string>> GetCachedPools()
    {
        IEnumerable<string> pools = new List<string>();

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
            var availableCoinsFromBaseCoin = swapPoolsResponse.Data
                                                              .Where( sp => sp.Assets.Contains( BaseCoin ) )
                                                              .Select( sp => sp.Assets
                                                                          .Where( p => !p.Equals( BaseCoin ) )
                                                                         ?.FirstOrDefault() ?? "" );

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

    public async Task<Result<IEnumerable<string>>> GetAll()
    {
        return Result.Ok( await GetCachedPools() );
    }
}