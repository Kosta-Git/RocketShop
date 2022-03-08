using Binance.Net.Interfaces.Clients;
using Microsoft.Extensions.Logging;
using Models.Results;

namespace BLL.Services.Funds;

public class FundService : IFundService
{
    private readonly IBinanceClient _client;
    private readonly ILogger<FundService> _logger;

    public FundService( IBinanceClient client, ILogger<FundService> logger )
    {
        _client = client;
        _logger = logger;
    }

    public async Task<Result<decimal>> GetAvailable( string asset )
    {
        var coinsResponse = await _client.SpotApi.Account.GetUserAssetsAsync();

        if ( !coinsResponse.Success ) return Result.Fail<decimal>( "Unable to fetch assets" );

        var assets = coinsResponse.Data;


        return Result.Ok(
            assets.FirstOrDefault( a => a.Asset.Equals( asset, StringComparison.InvariantCultureIgnoreCase ) )
                 ?.Available ?? 0 );
    }

    public async Task<Result> IsAvailable( string asset, decimal amount )
    {
        var available = await GetAvailable( asset );

        if ( available.Failure ) return Result.Fail( "Not enough funds" );

        return available.Value >= amount ? Result.Ok() : Result.Fail( "Not enough funds" );
    }
}