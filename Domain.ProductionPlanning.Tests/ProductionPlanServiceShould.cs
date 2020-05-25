using Application.ProductionPlan.Shared;
using Domain.ProductionPlanning.Models;
using Domain.ProductionPlanning.Services;
using Domain.ProductionPlanning.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using FuelsModel = Application.ProductionPlan.Shared.Models.Fuels;
using PowerPlantModel = Application.ProductionPlan.Shared.Models.PowerPlant;

namespace Domain.ProductionPlanning.Tests
{
    [TestClass]
    public class ProductionPlanServiceShould
    {
        private readonly IProductionPlanningService _productionPlanService;

        public ProductionPlanServiceShould()
        {
            _productionPlanService = new ProductionPlanPlanningService();
        }

        private static (decimal load, ICollection<PowerPlant> powerPlants) GetDataFromPayload(string path)
        {
            using var file = File.OpenText(path);
            var requestObject = JsonConvert.DeserializeObject<ProductionPlanRequest>(file.ReadToEnd());

            return (requestObject.Load, CreatePowerPlants(requestObject.PowerPlants, MapFuels(requestObject.Fuels)));
        }

        private static Fuels MapFuels(FuelsModel fuels)
            => new Fuels { GasPrice = fuels.Gas, KerosinePrice = fuels.Kerosine, C02EmissionConst = fuels.Co2, PercentageOfWind = fuels.Wind };

        private static ICollection<PowerPlant> CreatePowerPlants(IEnumerable<PowerPlantModel> powerPlants, Fuels fuels)
            => powerPlants.Select(p => new PowerPlant(p.Name, p.Type, fuels, p.Efficiency, p.PMin, p.PMax))
                .ToList();

        private static IEnumerable<object[]> Payloads()
        {
            yield return new object[] { "payload1.json" };
            yield return new object[] { "payload2.json" };
            yield return new object[] { "payload3.json" };
        }

        [DataTestMethod]
        [DynamicData(nameof(Payloads), DynamicDataSourceType.Method)]
        public void Return_A_Result_With_A_Power_Sum_Equal_To_Requested_Load(string jsonFile)
        {
            // Arrange
            var (load, powerPlants) = GetDataFromPayload(Path.Combine(Directory.GetCurrentDirectory(), jsonFile));
            var expectedLoad = load;

            // Act
            var results = _productionPlanService.CalculateUnitCommitment(load, powerPlants);

            // Assert
            Assert.AreEqual(expectedLoad, results.PowerPlants.Sum(p => p.EstimatedPower));
        }

        [DataTestMethod]
        [DynamicData(nameof(Payloads), DynamicDataSourceType.Method)]
        public void Return_Results_With_PowerEstimations_0_Or_GreaterThan_Or_EqualTo_Pmin(string jsonFile)
        {
            // Arrange
            var (load, powerPlants) = GetDataFromPayload(Path.Combine(Directory.GetCurrentDirectory(), jsonFile));

            // Act
            var productionPlan = _productionPlanService.CalculateUnitCommitment(load, powerPlants);

            // Assert
            foreach (var powerPlant in productionPlan.PowerPlants)
            {
                Assert.IsTrue(powerPlant.EstimatedPower == 0 || powerPlant.EstimatedPower >= powerPlant.PMin,
                    $"EstimatedPower: {powerPlant.EstimatedPower} is lower than Pmin: {powerPlant.PMin} for: {powerPlant.Name}");
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(Payloads), DynamicDataSourceType.Method)]
        public void Return_Results_With_PowerEstimation_SmallerThan_Or_EqualTo_Pmax(string jsonFile)
        {
            // Arrange
            var (load, powerPlants) = GetDataFromPayload(Path.Combine(Directory.GetCurrentDirectory(), jsonFile));

            // Act
            var productionPlan = _productionPlanService.CalculateUnitCommitment(load, powerPlants);

            // Assert
            foreach (var powerPlant in productionPlan.PowerPlants)
            {
                Assert.IsTrue(powerPlant.EstimatedPower <= powerPlant.PMax,
                    $"EstimatedPower: {powerPlant.EstimatedPower} is Greater than Pmax: {powerPlant.PMax} for: {powerPlant.Name}");
            }
        }
    }
}