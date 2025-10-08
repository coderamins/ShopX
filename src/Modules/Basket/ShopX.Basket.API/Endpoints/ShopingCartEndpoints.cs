using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ShopX.Basket.Application.ShoppingCarts.Queries.GetShopingCarts;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;

namespace ShopX.Basket.API.Endpoints
{
    public static class ShopingCartEndpoints
    {
        public static IEndpointRouteBuilder MapShopingCartEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1/basket")
            .WithTags("ShopingCart");

            group.MapGet("/{buyerId}", async (string buyerId, [FromServices] ISender sender) =>
            {
                var cart = await sender.Send(new GetShoppingCartQuery(buyerId));
                return cart is null ? Results.NotFound() : Results.Ok(cart);
            });

            group.MapPost("/", async ([FromBody]CreateShopingCartCommand cmd,[FromServices] ISender sender) =>
            {
                var id = await sender.Send(cmd);
                return Results.Created($"/api/v1/basket/{id}", new { id });
            });

            group.MapPost("/{buyerId}/items", async (string buyerId,[FromBody] AddItemToCartCommand cmd,[FromServices] ISender sender) =>
            {
                var commandWithBuyerId = cmd with { BuyerId = buyerId };
                await sender.Send(commandWithBuyerId);
                return Results.NoContent();
            });

            group.MapDelete("/{buyerId}/items/{productId:guid}", async (string buyerId, Guid productId,[FromServices] ISender sender) =>
            {
                await sender.Send(new RemoveItemFromCartCommand(buyerId, productId));
                return Results.NoContent();
            });


            return app;


        }
    }
}
