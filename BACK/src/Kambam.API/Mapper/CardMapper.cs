using AutoMapper;
using Kambam.Domain.Entities;
using Kambam.Service.Dtos;

namespace Kambam.API.Mapper;

public class CardMapperProfile : Profile
{
    public CardMapperProfile()
    {
        CreateMap<CardEntity, CardDto>().ReverseMap();
        CreateMap<CardEntity, CardWithIdDto>().ReverseMap();
    }
}