using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Configurations;

public static class HealthCheckExtensions
{
    public static IEndpointRouteBuilder EnableHealthChecks( this IEndpointRouteBuilder builder )
    {
        return builder.InitLiveCheck().InitReadyCheck();
    }

    public static IEndpointRouteBuilder InitLiveCheck( this IEndpointRouteBuilder builder )
    {
        builder.MapHealthChecks(
            "/health/live",
            new HealthCheckOptions
            {
                Predicate = _ => false
            }
        );

        return builder;
    }

    public static IEndpointRouteBuilder InitReadyCheck( this IEndpointRouteBuilder builder )
    {
        builder.MapHealthChecks(
            "/health/ready",
            new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains( "ready" ),
                ResponseWriter = async ( context, response ) =>
                {
                    var result = new
                    {
                        status = response.Status.ToString(),
                        checks = response.Entries.Select( e => new
                        {
                            name      = e.Key,
                            status    = e.Value.Status.ToString(),
                            exception = e.Value.Exception != null ? e.Value.Exception.Message : "none",
                            duration  = e.Value.Duration.ToString()
                        } )
                    };

                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    await context.Response.WriteAsJsonAsync( result );
                }
            }
        );

        return builder;
    }
}