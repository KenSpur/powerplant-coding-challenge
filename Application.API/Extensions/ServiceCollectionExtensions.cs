using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Application.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PowerPlant Coding Challenge",
                    Version = "v1",
                    Description = "Challenge submission",
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}