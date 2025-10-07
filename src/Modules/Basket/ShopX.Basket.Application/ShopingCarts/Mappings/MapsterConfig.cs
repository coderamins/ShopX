using Mapster;
using ShopX.Basket.Application.ShopingCarts.DTOs;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Basket.Application.ShopingCarts.Mappings
{
    public static class MapsterConfig
    {
        public static TypeAdapterConfig CreateMapper()
        {
            var config = new TypeAdapterConfig();

            // ثبت مپینگ‌ها
            //config.NewConfig<ShoppingCart, ShoppingCartDto>()
            // .ConstructUsing(src => new ShoppingCartDto(
            //     src.Id,
            //     src.BuyerId ,
            //     src.Items
            // ));

            return config;
        }
    }
}
