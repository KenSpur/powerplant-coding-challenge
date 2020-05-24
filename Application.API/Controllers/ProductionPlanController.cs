using Application.ProductionPlan.Shared;
using Domain.ProductionPlanning.Models;
using Domain.ProductionPlanning.Models.Enums;
using Domain.ProductionPlanning.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanningService _productionPlanService;

        public ProductionPlanController(IProductionPlanningService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductionPlanRequest body)
        {
            var load = body.Load;
            var fuel = new Fuels
            {
                GasPrice = body.Fuels.Gas,
                KerosinePrice = body.Fuels.Kerosine,
                C02EmissionConst = body.Fuels.Co2,
                PercentageOfWind = body.Fuels.Wind
            };

            var powerPlants = body.PowerPlants.Select(p =>
                new PowerPlant(
                    p.Name,
                    (PowerPlantType)Enum.Parse(typeof(PowerPlantType), p.Type, true),
                    fuel,
                    p.Efficiency,
                    p.PMin,
                    p.PMax)).ToList();

            var productionPlan = _productionPlanService.CalculateUnitCommitment(load, powerPlants);

            if (productionPlan.IsSuccessFul)
                return Ok(productionPlan); //Map production plan

            return BadRequest(productionPlan.ErrorMessage);
        }
    }
}