using Domain.ProductionPlanning.Models;
using System.Collections.Generic;

namespace Domain.ProductionPlanning.Services
{
    public interface IProductionPlanningService
    {
        ProductionPlan CalculateUnitCommitment(decimal load, ICollection<PowerPlant> powerplants);
    }
}