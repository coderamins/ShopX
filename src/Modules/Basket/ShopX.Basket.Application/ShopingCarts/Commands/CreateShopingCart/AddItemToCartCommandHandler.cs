using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Domain.Entities;
using ShopX.Basket.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record AddItemToCartCommand(string BuyerId, Guid ProductId, int Quantity, decimal UnitPrice) : IRequest<Unit>;

    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand,Unit>
    {
        private readonly BasketDbContext _db;
        private readonly CatalogService _catalog;

        public AddItemToCartCommandHandler(BasketDbContext db) => _db = db;

        public async Task<Unit> Handle(AddItemToCartCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
            .Include(c => c.Items).AsTracking()
            .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
            {
                cart = new ShoppingCart(request.BuyerId);
                _db.ShoppingCarts.Add(cart);
            }

            var existing = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existing is null)
            {
                var newItem = new ShoppingCartItem(request.ProductId, request.UnitPrice, request.Quantity);
                cart.Items.Add(newItem);
                _db.ShoppingCartItems.Add(newItem); // 👈 این خط مشکل Concurrency رو حل می‌کنه
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
