using Mapster;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Products.DTOs;

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
