using Newtonsoft.Json;

namespace Application.ProductionPlan.Shared.Models
{
    public class Fuels
    {
        /// <summary>
        /// gas(euro/MWh)
        /// </summary>
        [JsonProperty("gas(euro/MWh)")]
        public decimal Gas { get; set; }
        /// <summary>
        /// kerosine(euro/MWh)
        /// </summary>
        [JsonProperty("kerosine(euro/MWh)")]
        public decimal Kerosine { get; set; }
        /// <summary>
        /// co2(euro/ton)
        /// </summary>
        [JsonProperty("co2(euro/ton)")]
        public decimal Co2 { get; set; }
        /// <summary>
        /// wind(%)
        /// </summary>
        [JsonProperty("wind(%)")]
        public decimal Wind { get; set; }
    }
}