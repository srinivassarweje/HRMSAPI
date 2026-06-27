using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HRMSAPI.Extensions
{
    public static class OpenApiExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            // Register minimal OpenAPI support (endpoints explorer). Swashbuckle may not be available in the build environment.
            services.AddEndpointsApiExplorer();

            return services;
        }

        public static WebApplication MapOpenApi(this WebApplication app)
        {
            // If Swashbuckle is available in your environment, you can enable Swagger middleware here.
            // For build reliability in environments without the Swashbuckle package available, this is a no-op.
            return app;
        }
    }
}