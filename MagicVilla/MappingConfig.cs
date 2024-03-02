using AutoMapper;
using MagicVilla.Models;
using MagicVilla.Models.Dto;

namespace MagicVilla
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<VillaCreateDTO, Villa>().ReverseMap();
            CreateMap<VillaUpdateDTO, Villa>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumber>().ReverseMap();
            CreateMap<VillaNumberUpdateDTO, VillaNumber>().ReverseMap();
        }
    }
}
