using Domain.ProductionPlanning.Services;
using Domain.ProductionPlanning.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.ProductionPlanning
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductionPlanning(this IServiceCollection service)
        {
            service.AddTransient<IProductionPlanningService, ProductionPlanPlanningService>();

            return service;
        }
    }
}