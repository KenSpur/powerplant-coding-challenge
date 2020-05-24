using Newtonsoft.Json;

namespace Application.ProductionPlan.Shared.Models
{
    public class PowerPlant
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("efficiency")]
        public decimal Efficiency { get; set; }
        [JsonProperty("pmin")]
        public decimal PMin { get; set; }
        [JsonProperty("pmax")]
        public decimal PMax { get; set; }
    }
}