using Domain.ProductionPlanning.Defaults;
using Domain.ProductionPlanning.Extensions;
using Domain.ProductionPlanning.Models;
using System.Collections.Generic;
using System.Linq;

namespace Domain.ProductionPlanning.Services.Implementations
{
    public class ProductionPlanPlanningService : IProductionPlanningService
    {
        public ProductionPlan CalculateUnitCommitment(decimal requiredLoad, ICollection<PowerPlant> powerPlants)
        {
            if (!powerPlants.Any(p => p.PMin < requiredLoad))
                return new ProductionPlan(requiredLoad, powerPlants, InternalProductionPlanningDefaults
                    .CantMeetLoadMessage(powerPlants.EstimatedPower() - requiredLoad));

            if (TrySetCheapestPowerSourcesToMaxUntilLoadMet(requiredLoad, powerPlants))
                return new ProductionPlan(requiredLoad, powerPlants);

            if (powerPlants.EstimatedPower() < requiredLoad)
                return new ProductionPlan(requiredLoad, powerPlants, InternalProductionPlanningDefaults
                    .CantMeetLoadMessage(powerPlants.EstimatedPower() - requiredLoad));

            if (TryDecreaseMostCostlyOverShotPower(requiredLoad, powerPlants))
                return new ProductionPlan(requiredLoad, powerPlants);

            return TryTweakWithMoreCostlyPowerSources(requiredLoad, powerPlants)
                ? new ProductionPlan(requiredLoad, powerPlants)
                : new ProductionPlan(requiredLoad, powerPlants, InternalProductionPlanningDefaults
                    .CurrentAlgorithmIsNotAccurateEnoughMessage(powerPlants.EstimatedPower() - requiredLoad));
        }

        private static bool TrySetCheapestPowerSourcesToMaxUntilLoadMet(decimal requiredLoad, ICollection<PowerPlant> powerPlants)
        {
            foreach (var powerPlant in powerPlants
                .OrderBy(p => p.MwhPrice)
                .ThenBy(p => p.PMin))
            {
                powerPlant.SetEstimatedPowerToMax();

                if (powerPlants.EstimatedPower() >= requiredLoad) break;
            }

            return requiredLoad == powerPlants.EstimatedPower();
        }

        private static bool TryDecreaseMostCostlyOverShotPower(decimal requiredLoad, ICollection<PowerPlant> powerPlants)
        {
            var overShotValue = powerPlants.EstimatedPower() - requiredLoad;
            foreach (var powerPlant in powerPlants.Where(p => p.EstimatedPower != 0)
                .OrderByDescending(p => p.MwhPrice)
                .ThenBy(p => p.PMin))
            {

                if (!powerPlant.TryDecreaseCurrentEstimatedPowerBy(overShotValue, out var remainder, true))
                    continue;

                overShotValue = remainder;

                if (remainder == 0) break;
            }

            return overShotValue == 0;
        }

        private static bool TryTweakWithMoreCostlyPowerSources(decimal requiredLoad, ICollection<PowerPlant> powerPlants)
        {
            foreach (var powerPlant in powerPlants
                .Where(p => p.EstimatedPower != 0)
                .OrderByDescending(p => p.MwhPrice)
                .ThenBy(p => p.PMin))
            {
                powerPlant.DecreaseEstimatedPowerToZero();

                if (TrySetCheapestPowerSourcesToMaxUntilLoadMet(requiredLoad, powerPlants.Where(p => p.Id != powerPlant.Id).ToList()))
                    return true;

                if (powerPlants.EstimatedPower() < requiredLoad)
                    break;

                if (TryDecreaseMostCostlyOverShotPower(requiredLoad, powerPlants))
                    return true;
            }

            return false;
        }
    }
}