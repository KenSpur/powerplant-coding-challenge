using System.Collections.Generic;
using System.Linq;

namespace Domain.ProductionPlanning.Models
{
    public class ProductionPlan
    {
        private readonly decimal _load;

        public ProductionPlan(decimal load, IEnumerable<PowerPlant> powerPlants, string errorMessage = null)
        {
            _load = load;
            PowerPlants = powerPlants.ToList();
            ErrorMessage = errorMessage;
        }

        public ICollection<PowerPlant> PowerPlants { get; set; }
        public bool IsSuccessFul => _load == PowerPlants.Sum(p => p.EstimatedPower);
        public string ErrorMessage { get; set; }
    }
}