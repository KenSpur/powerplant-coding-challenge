using Domain.ProductionPlanning.Models;
using System.Collections.Generic;
using System.Linq;

namespace Domain.ProductionPlanning.Extensions
{
    public static class CollectionOfPowerPlantsExtensions
    {
        public static decimal EstimatedPower(this ICollection<PowerPlant> powerPlants)
            => powerPlants.Sum(p => p.EstimatedPower);
    }
}