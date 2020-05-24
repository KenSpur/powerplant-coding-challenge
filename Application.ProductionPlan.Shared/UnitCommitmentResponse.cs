using Newtonsoft.Json;

namespace Application.ProductionPlan.Shared
{
    public class UnitCommitmentResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("p")]
        public decimal EstimatedPower { get; set; }
    }
}