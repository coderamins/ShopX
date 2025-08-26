using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.Mappings
{
    public static class ProductMappings
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<Product, ProductDto>.NewConfig();
        }
    }
}
