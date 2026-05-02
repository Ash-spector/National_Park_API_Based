using AutoMapper;
using National_Park_API.Models;
using National_Park_API.Models.DTos;

namespace National_Park_API.DToMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalParkDto, National_Park>().ReverseMap();
            CreateMap<TrailDTo, Trail>().ReverseMap();
        }
    }
}