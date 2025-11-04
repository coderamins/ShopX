using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Application.Common.Interfaces;

namespace ShopX.Basket.Application.ShopingCarts.Commands.AddItemToCart
{
    // Features/ShoppingCart/Commands/AddItemToCart/AddItemToCartCommandHandler.cs
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, Unit>
    {
        private readonly IBasketDbContext _db;
        private readonly ICatalogService _catalog;

        public AddItemToCartCommandHandler(IBasketDbContext db, ICatalogService catalog)
        {
            _db = db;
            _catalog = catalog;
        }

        public async Task<Unit> Handle(AddItemToCartCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .AsTracking()
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
            {
                cart = new Domain.Entities.ShoppingCart(request.BuyerId);
                _db.ShoppingCarts.Add(cart);
            }

            // گرفتن اطلاعات محصول از سرویس کاتالوگ
            var product = await _catalog.GetProductByIdAsync(request.ProductId, ct);
            if (product is null)
                throw new InvalidOperationException("محصول مورد نظر یافت نشد.");

            var existing = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existing is null)
            {
                var newItem = new Domain.Entities.ShoppingCartItem(request.ProductId, product.Price, request.Quantity);
                cart.Items.Add(newItem);
                _db.ShoppingCartItems.Add(newItem);
            }
            else
            {
                existing.AddQuantity(request.Quantity);
            }

            await _db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
