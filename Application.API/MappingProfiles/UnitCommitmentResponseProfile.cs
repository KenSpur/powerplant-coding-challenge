using Application.ProductionPlan.Shared;
using AutoMapper;
using Domain.ProductionPlanning.Models;

namespace Application.API.MappingProfiles
{
    public class UnitCommitmentResponseProfile : Profile
    {
        public UnitCommitmentResponseProfile()
        {
            CreateMap<PowerPlant, UnitCommitmentResponse>()
                .ForMember(dest => dest.Name, opt => opt
                        .MapFrom(src => src.Name))
                .ForMember(dest => dest.EstimatedPower, opt => opt
                        .MapFrom(src => src.EstimatedPower));
        }
    }
}