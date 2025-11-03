using MediatR;
using ShopX.Basket.Application.ShopingCarts.DTOs;

namespace ShopX.Basket.Application.ShoppingCarts.Queries.GetShopingCarts
{
    public record GetShoppingCartQuery(string BuyerId) : IRequest<ShoppingCartDto?>;

}
