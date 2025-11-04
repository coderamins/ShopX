using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Application.Common.Interfaces;
using ShopX.Basket.Application.DTOs;
using ShopX.Basket.Application.ShopingCarts.DTOs;

namespace ShopX.Basket.Application.ShoppingCarts.Queries.GetShopingCarts
{
    public class GetShoppingCartQueryHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCartDto?>
    {
        private readonly IBasketDbContext _db;

        public GetShoppingCartQueryHandler(IBasketDbContext db) => _db = db;

        public async Task<ShoppingCartDto?> Handle(GetShoppingCartQuery request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                return null;

            return new ShoppingCartDto
            {
                BuyerId = cart.BuyerId,
                Items = cart.Items.Select(i => new ShoppingCartItemDto
                {                    
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,                    
                    //ProductName=i.pro
                }).ToList()
            };
        }
    }
}
