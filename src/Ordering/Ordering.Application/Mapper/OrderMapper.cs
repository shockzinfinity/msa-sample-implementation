using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Ordering.Application.Mapper
{
  // https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
  public static class OrderMapper
  {
    public static IMapper Mapper => Lazy.Value;

    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
      var config = new MapperConfiguration(cfg =>
      {
        // this line ensures that internal properties are also mapped over.
        cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
        cfg.AddProfile<OrderMappingProfile>();
      });

      var mapper = config.CreateMapper();
      return mapper;
    });
  }
}
