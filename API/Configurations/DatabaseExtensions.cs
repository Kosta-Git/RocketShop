using System;
using System.Threading.Tasks;
using DataAccess.DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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

        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IValidationRuleRepository, ValidationRuleRepository>();

        return service;
    }

    public static async Task RunDatabaseMigrations( this IApplicationBuilder app )
    {
        Log.Information("Attempting to run database migrations...");

        try
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            await serviceScope?.ServiceProvider.GetRequiredService<DatabaseContext>().Database.MigrateAsync()!;
        }
        catch ( Exception ex )
        {
            Log.Error( ex, "Migrations were not applied, please try to apply them manually!" );
        }
        
    }
}