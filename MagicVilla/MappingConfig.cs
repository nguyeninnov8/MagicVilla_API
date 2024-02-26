using AutoMapper;
using MagicVilla.Models;
using MagicVilla.Models.Dto;

namespace MagicVilla
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaCreateDTO, VillaDTO>().ReverseMap();
            CreateMap<VillaUpdateDTO, VillaDTO>().ReverseMap();
        }
    }
}
