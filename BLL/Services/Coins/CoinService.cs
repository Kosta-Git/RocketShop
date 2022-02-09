using System.Collections;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Spot.WalletData;
using DataAccess.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace BLL.Services.Coins;

public class CoinService : ICoinService
{
    private readonly IMemoryCache _cache;
    private readonly IBinanceClient _client;
    private readonly ILogger<CoinService> _logger;

    public static readonly string CacheKey = nameof( CoinService );

    public CoinService( IMemoryCache cache, IBinanceClient client, ILogger<CoinService> logger )
    {
        _cache  = cache;
        _client = client;
        _logger = logger;

        GetCachedCoins().Wait();
    }

    private async Task<Dictionary<string, BinanceUserCoin>> GetCachedCoins()
    {
        Dictionary<string, BinanceUserCoin> coins = new();

        // Try fetching coins
        if ( !_cache.TryGetValue( CacheKey, out coins ) )
        {
            // If coins are not present, try to fetch them
            await FetchCoins();
            _cache.TryGetValue( CacheKey, out coins );
        }

        return coins;
    }

    private async Task FetchCoins()
    {
        // Fetch all available coins the account can interact with
        var coinsResponse = await _client.General.GetUserCoinsAsync();

        // Check if the response was a success
        if ( coinsResponse.Success )
        {
            // We can cache the response, and make it expire in 5mns
            using var coinsEntry = _cache.CreateEntry( CacheKey );


            coinsEntry.Value = coinsResponse.Data.ToDictionary( k => k.Coin.ToUpperInvariant(), c => c );
            coinsEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes( 5 );

            // Log that cache was refreshed
            _logger.LogDebug( "Coins cache was set/refreshed" );
        }
        else
        {
            _logger.LogError( "Unable to fetch user coins." );
        }
    }

    public async Task<Result<IEnumerable<BinanceUserCoin>>> GetCoins()
    {
        return Result.Ok( ( await GetCachedCoins() ).Values.AsEnumerable() );
    }

    public async Task<Result<BinanceUserCoin>> GetCoin( string coin )
    {
        var coins = await GetCachedCoins();
        return !coins.TryGetValue( coin.ToUpperInvariant(), out var cachedCoin )
            ? Result.Fail<BinanceUserCoin>( "Coin was not found" )
            : Result.Ok( cachedCoin );
    }

    public async Task<Result<IEnumerable<BinanceNetwork>>> GetCoinNetworks( string coin )
    {
        var cachedCoin = await GetCoin( coin );

        return cachedCoin.Failure
            ? Result.Fail<IEnumerable<BinanceNetwork>>( "Coin was not found" )
            : Result.Ok( cachedCoin.Value.NetworkList );
    }
}