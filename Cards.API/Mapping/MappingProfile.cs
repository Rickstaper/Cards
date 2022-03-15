using AutoMapper;
using Cards.Entity.DataTransferObject;
using Cards.Entity.Models;

namespace Cards.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDto>();

            CreateMap<CardForCreationDto, Card>();

            CreateMap<CardForUpdateDto, Card>();
        }
    }
}
