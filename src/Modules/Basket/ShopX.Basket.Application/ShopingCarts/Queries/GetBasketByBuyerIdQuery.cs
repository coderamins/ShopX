using MediatR;
using ShopX.Basket.Application.ShopingCarts.DTOs;

namespace ShopX.Basket.Application.Queries;

public sealed record GetBasketByBuyerIdQuery(Guid BuyerId) : IRequest<ShoppingCartDto?>;
