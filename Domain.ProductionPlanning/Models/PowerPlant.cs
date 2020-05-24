using Domain.ProductionPlanning.Models.Enums;
using System;

namespace Domain.ProductionPlanning.Models
{
    public class PowerPlant
    {
        private readonly decimal _pmax;

        public PowerPlant(string name, PowerPlantType type, Fuels fuels, decimal efficiency, decimal pmin, decimal pmax)
        {
            Id = Guid.NewGuid();
            _pmax = pmax;
            Name = name;
            Type = type;
            Fuels = fuels;
            Efficiency = efficiency;
            PMin = pmin;
        }

        internal Guid Id { get; }
        public string Name { get; }
        public PowerPlantType Type { get; }
        public Fuels Fuels { get; }
        public decimal Efficiency { get; }
        public decimal PMin { get; }
        public decimal PMax
            => Type switch
            {
                PowerPlantType.GasFired => _pmax,
                PowerPlantType.TurboJet => _pmax,
                PowerPlantType.WindTurbine => _pmax * ((decimal)Fuels.PercentageOfWind / 100),
                _ => throw new ArgumentOutOfRangeException()
            };
        public decimal MwhPrice
            => Type switch
            {
                PowerPlantType.GasFired => Fuels.GasPrice / Efficiency + Fuels.C02EmissionConst * (decimal)0.3,
                PowerPlantType.TurboJet => Fuels.KerosinePrice / Efficiency,
                PowerPlantType.WindTurbine => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        public decimal EstimatedPower { get; private set; }

        internal void SetEstimatedPowerToMax()
            => EstimatedPower = PMax;

        internal void DecreaseEstimatedPowerToZero()
            => EstimatedPower = 0;

        internal bool TryDecreaseCurrentEstimatedPowerBy(decimal requiredPowerDecrease, out decimal remainder, bool keepActive = false)
        {
            remainder = requiredPowerDecrease;

            var possiblePowerDecrease = EstimatedPower;

            if (keepActive || requiredPowerDecrease < EstimatedPower)
                possiblePowerDecrease = EstimatedPower - PMin;

            if (possiblePowerDecrease == 0m) return false;

            if (possiblePowerDecrease < requiredPowerDecrease)
            {
                EstimatedPower -= possiblePowerDecrease;
                remainder -= possiblePowerDecrease;
                return true;
            }

            EstimatedPower -= requiredPowerDecrease;
            remainder -= requiredPowerDecrease;

            return true;
        }
    }
}