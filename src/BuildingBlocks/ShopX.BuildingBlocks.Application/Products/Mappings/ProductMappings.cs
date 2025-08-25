using Mapster;
using ShopX.BuildingBlocks.Application.Products.DTOs;
using ShopX.BuildingBlocks.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.BuildingBlocks.Application.Products.Mappings
{
    public static class ProductMappings
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<Product, ProductDto>.NewConfig();
        }
    }
}
