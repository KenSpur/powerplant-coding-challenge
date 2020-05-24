namespace Domain.ProductionPlanning.Models
{
    public class Fuels
    {
        /// <summary>
        /// euro/MWh
        /// </summary>
        public decimal GasPrice { get; set; }
        /// <summary>
        /// euro/MWh
        /// </summary>
        public decimal KerosinePrice { get; set; }
        /// <summary>
        /// euro/ton
        /// </summary>
        public decimal C02EmissionConst { get; set; }
        /// <summary>
        /// 0-100%
        /// </summary>
        public decimal PercentageOfWind { get; set; }
    }
}