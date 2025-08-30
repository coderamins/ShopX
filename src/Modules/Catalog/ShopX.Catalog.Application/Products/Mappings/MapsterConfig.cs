using Mapster;
using MapsterMapper;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.Mappings
{
    public static class MapsterConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new TypeAdapterConfig();

            // ثبت مپینگ‌ها
            config.NewConfig<Product, ProductDto>()
                  .Map(dest => dest.Id, src => src.Id.Value);

            return new Mapper(config);
        }
    }
}
