using System;
using API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace API;

public class Startup
{
    public Startup( IConfiguration configuration )
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices( IServiceCollection services )
    {
        services.InitBinance( Configuration ).InitDb( Configuration ).InitServices();
        services.AddControllers();
        services.AddSwaggerGen( c => { c.SwaggerDoc( "v1", new OpenApiInfo { Title = "API", Version = "v1" } ); } );
        services.AddHealthChecks()
                .AddNpgSql(
                     Configuration.GetConnectionString( "Default" ),
                     name: "Database",
                     timeout: TimeSpan.FromSeconds( 2 ),
                     tags: new[] { "ready" }
                 );
        services.AddCors( options =>
        {
            options.AddDefaultPolicy( p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );
        } );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
    {
        app.RunDatabaseMigrations().Wait();

        if ( env.IsDevelopment() )
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI( c => c.SwaggerEndpoint( "/swagger/v1/swagger.json", "API v1" ) );
        }

        app.UseHttpsRedirection();
        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseCors();
        app.UseAuthorization();
        app.UseEndpoints( endpoints =>
        {
            endpoints.MapControllers();
            endpoints.EnableHealthChecks();
        } );
    }
}