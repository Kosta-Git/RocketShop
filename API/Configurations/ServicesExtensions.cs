using BLL.Services.Coins;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations;

public static class ServicesExtensions
{
    public static IServiceCollection InitServices( this IServiceCollection service )
    {
        service.AddMemoryCache();
        service.AddScoped<ICoinService, CoinService>();

        return service;
    }
}