using Application.ProductionPlan.Shared;
using AutoMapper;
using Domain.ProductionPlanning.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.API.MappingProfiles
{
    public class ProductionPlanRequestProfile : Profile
    {
        public ProductionPlanRequestProfile()
        {
            CreateMap<ProductionPlanRequest, ICollection<PowerPlant>>()
                .ConstructUsing(request => request.PowerPlants.Select(p => new PowerPlant
                (
                    p.Name,
                    p.Type,
                    new Fuels
                    {
                        GasPrice = request.Fuels.Gas,
                        KerosinePrice = request.Fuels.Kerosine,
                        C02EmissionConst = request.Fuels.Co2,
                        PercentageOfWind = request.Fuels.Wind
                    },
                    p.Efficiency,
                    p.PMin,
                    p.PMax
                )).ToList());
        }
    }
}