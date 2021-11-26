using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Configurations;

public static class DatabaseExtensions
{
    public static IServiceCollection InitDb(
        this IServiceCollection service,
        IConfiguration configuration
    )
    {
        service.AddDbContext<DatabaseContext>( options =>
        {
            options.UseNpgsql( configuration.GetConnectionString( "Default" ) );
        } );

        return service;
    }
}