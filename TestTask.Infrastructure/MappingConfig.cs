using AutoMapper;
using TestTask.Application.Dtos;
using TestTask.Core.Entities;
using TestTask.Core.Utility;

namespace TestTask.Infrastructure;

internal class MappingConfig
{
  public static MapperConfiguration RegisterMaps()
  {
    var mappingConfig = new MapperConfiguration(config =>
    {
      config.CreateMap<ProductDto, Product>().ReverseMap()
        .ForMember(dest => dest.TotalPriceWithVat, p => 
          p.MapFrom(src => src.Price * src.Quantity * (VatValue.Value) / 100));
    });
    return mappingConfig;
  }
}