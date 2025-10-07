using Mapster;
using ShopX.Basket.Application.ShopingCarts.DTOs;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Basket.Application.ShopingCarts.Mappings
{
    public static class ProductMappings
    {
        public static void ConfigureMappings()
        {
            TypeAdapterConfig<ShoppingCart, ShoppingCartDto>.NewConfig();
        }
    }
}
