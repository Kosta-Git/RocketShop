using BLL.Services.Coins;
using BLL.Services.Funds;
using BLL.Services.SwapPools;
using BLL.Services.Swaps;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations;

public static class ServicesExtensions
{
    public static IServiceCollection InitServices( this IServiceCollection service )
    {
        service.AddMemoryCache();
        service.AddScoped<ICoinService, CoinService>();
        service.AddScoped<ISwapPoolsService, SwapPoolsService>();
        service.AddScoped<IFundService, FundService>();
        service.AddScoped<ISwapService, SwapService>();

        return service;
    }
}