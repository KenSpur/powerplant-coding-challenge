using Application.ProductionPlan.Shared.Models;
using Newtonsoft.Json;

namespace Application.ProductionPlan.Shared
{
    public class ProductionPlanRequest
    {
        [JsonProperty("load")]
        public decimal Load { get; set; }
        [JsonProperty("fuels")]
        public Fuels Fuels { get; set; }
        [JsonProperty("powerplants")]
        public PowerPlant[] PowerPlants { get; set; }
    }
}