using AutoMapper;
using WebApplicationBackend.DTOs;
using WebApplicationBackend.Models;

namespace WebApplicationBackend.AutoMapers
{
    public class MapingProfile:Profile
    {
        public MapingProfile()
        {
            CreateMap<BeerInsertDto, Beer>();

            CreateMap<Beer, BeerDto>()
                .ForMember(destDto => destDto.Id, opt => opt.MapFrom(b => b.BeerId));

            CreateMap<BeerUpdateDto, Beer>();
        }

    }
}
