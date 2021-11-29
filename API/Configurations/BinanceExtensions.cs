using Binance.Net;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API.Configurations;

public static class BinanceExtensions
{
    public static IServiceCollection InitBinance(
        this IServiceCollection service,
        IConfiguration configuration
    )
    {
        service.AddTransient<IBinanceClient>( _ => new BinanceClient( ClientOptions( configuration ) ) );
        return service;
    }

    private static BinanceClientOptions ClientOptions( IConfiguration configuration )
    {
        var key    = configuration.GetValue<string>( "Binance:Key" );
        var secret = configuration.GetValue<string>( "Binance:Secret" );
        var domain = configuration.GetValue<string>( "Binance:Domain" );

        return new BinanceClientOptions
        {
            ApiCredentials = new ApiCredentials( key, secret ),
            LogLevel       = LogLevel.Information,
            BaseAddress    = $"https://{domain}"
        };
    }
}