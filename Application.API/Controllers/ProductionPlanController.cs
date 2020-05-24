using Application.ProductionPlan.Shared;
using AutoMapper;
using Domain.ProductionPlanning.Models;
using Domain.ProductionPlanning.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace Application.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductionPlanningService _productionPlanService;

        public ProductionPlanController(IMapper mapper, IProductionPlanningService productionPlanService)
        {
            _mapper = mapper;
            _productionPlanService = productionPlanService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UnitCommitmentResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ProductionPlanRequest body)
        {
            var load = body.Load;
            var powerPlants = _mapper.Map<ICollection<PowerPlant>>(body);

            var productionPlan = _productionPlanService.CalculateUnitCommitment(load, powerPlants);

            if (productionPlan.IsSuccessFul)
                return Ok(_mapper.Map<ICollection<UnitCommitmentResponse>>(productionPlan.PowerPlants));

            return BadRequest(productionPlan.ErrorMessage);
        }
    }
}