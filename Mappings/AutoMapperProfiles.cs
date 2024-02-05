using AutoMapper;
using Project1.Models.Domain;
using Project1.Models.DTO;

namespace Project1.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Regions, RegionDto>().ReverseMap();
            CreateMap<CreateRegionDto,Regions>().ReverseMap();
            CreateMap<UpdateRegionRto, Regions>().ReverseMap();
        }
    }
}
