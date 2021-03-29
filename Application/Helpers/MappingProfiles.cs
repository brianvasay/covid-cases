using Application.Entities;
using Application.Entities.Dtos;
using AutoMapper;

namespace Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Case, CaseToReturnDto>()
                .ForMember(c => c.ObservationDate, d => d.MapFrom(x => string.Format("{0:yyyy-MM-dd}", x.ObservationDate)))
                .ForMember(c => c.Country, d => d.MapFrom(x => x.CountryRegion));
        }
    }
}
