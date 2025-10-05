using Microsoft.AspNetCore.Routing;

namespace ShopX.Basket.API.Endpoints
{
    public static class ShopingCartEndpoints
    {
        public static IEndpointRouteBuilder MapShopingCartEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/basket")
                .WithTags("ShopingCart");

            group.MapGet("/buyerId", async (string buyerId, ISender sender) =>
            {
                var cart = await sender.Send(new GetShoppingCartQuery(buyerId));
                return cart is null Results.NotFound():Results.Ok();
            });
        }
    }
}
