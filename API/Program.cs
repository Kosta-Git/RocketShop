using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API;

public class Program
{
    public static int Main( string[] args )
    {
        var configuration = new ConfigurationBuilder()
                           .AddJsonFile( "appsettings.json" )
                           .Build();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration( configuration ).CreateLogger()
                                              .ForContext<Program>();

        try
        {
            Log.Information( "Starting API." );
            CreateHostBuilder( args ).Build().Run();
            Log.Information( "Shutting down API." );
            return 0;
        }
        catch ( Exception ex )
        {
            Log.Fatal( ex, "The API was not able to start properly." );
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder( string[] args )
    {
        return Host.CreateDefaultBuilder( args )
                   .UseSerilog()
                   .ConfigureWebHostDefaults( webBuilder => webBuilder.UseStartup<Startup>() );
    }
}