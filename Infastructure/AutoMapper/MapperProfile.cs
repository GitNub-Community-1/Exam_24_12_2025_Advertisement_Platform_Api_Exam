using AutoMapper;
using Domain.Dtos;
using Domain.Models.Entity;

namespace Infastructure.AutoMapper;


public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CategoryDto,Category>().ReverseMap();
        CreateMap<CategoryCreatDto,Category>().ReverseMap();
        CreateMap<AdDto,Advertisement>().ReverseMap();
        CreateMap<AdCreatDto,Advertisement>().ReverseMap();
    }
    //Making true structure by  Abubakr
}