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
                    Title = "PowerPlant Coding Challenge Submission",
                    Version = "v1",
                    Description = "Submission for: <a href=\"https://github.com/gem-spaas/powerplant-coding-challenge \" target=\"_blank\">powerplant-coding-challenge</a>",
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }
    }
}