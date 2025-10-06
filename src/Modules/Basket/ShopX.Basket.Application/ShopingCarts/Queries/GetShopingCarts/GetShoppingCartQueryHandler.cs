using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Infrastructure.Persistence;
using ShopX.BuildingBlocks.Application.Products.DTOs;
using ShopX.Catalog.Application.Products.DTOs;

namespace ShopX.Catalog.Application.Products.Queries.GetShopingCarts
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartDto?>
    {
        private readonly BasketDbContext _db;

        public GetShoppingCartQueryHandler(BasketDbContext db) => _db = db;

        public async Task<ShoppingCartDto?> Handle(GetShoppingCartQuery request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            return cart is null ? null : new ShoppingCartDto(
                cart.Id,
                cart.BuyerId,
                cart.Items.Select(i => new BasketItemDto(i.ProductId, i.Quantity, i.UnitPrice)).ToList()
            );
        }
    }
}
