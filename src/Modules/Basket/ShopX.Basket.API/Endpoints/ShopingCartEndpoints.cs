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

            group.MapGet("/{buyerId}", async (string buyerId, ISender sender) =>
            {
                var cart = await sender.Send(new GetShoppingCartQuery(buyerId));
                return cart is null ? Results.NotFound() : Results.Ok(cart);
            });

            group.MapPost("/", async (CreateShopingCartCommand cmd, ISender sender) =>
            {
                var id = await sender.Send(cmd);
                return Results.Created($"/api/v1/basket/{id}", new { id });
            });

            group.MapPost("/{buyerId}/items", async (string buyerId, AddItemToCartCommand cmd, ISender sender) =>
            {
                // مطمئن شو که BuyerId داخل cmd مقداردهی می‌شود
                var commandWithBuyerId = cmd with { BuyerId = buyerId };
                await sender.Send(commandWithBuyerId);
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
