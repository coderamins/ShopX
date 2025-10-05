using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Routing;

namespace ShopX.Basket.API.Endpoints
{
    public static class ShopingCartEndpoints
    {
        public static IEndpointRouteBuilder MapShopingCartEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/basket")
            .WithTags("ShopingCart");

            group.MapGet("/{buyerId}", async (string buyerId, ISender sender) =>
            {
                var cart = await sender.Send(new GetShoppingCartQuery(buyerId));
                return cart is null ? Results.NotFound() : Results.Ok(cart);
            });

            group.MapPost("/", async (CreateShoppingCartCommand cmd, ISender sender) =>
            {
                var id = await sender.Send(cmd);
                return Results.Created($"/api/v1/basket/{id}", new { id });
            });

            group.MapPost("/{buyerId}/items", async (string buyerId, AddItemToCartCommand cmd, ISender sender) =>
            {
                await sender.Send(cmd with { BuyerId = buyerId });
                return Results.NoContent();
            });

            group.MapDelete("/{buyerId}/items/{productId:guid}", async (string buyerId, Guid productId, ISender sender) =>
            {
                await sender.Send(new RemoveItemFromCartCommand(buyerId, productId));
                return Results.NoContent();
            });

            return app;


        }
    }
}
