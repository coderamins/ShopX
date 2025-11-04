using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Application.Common.ShopX.Basket.Application.Common.Interfaces;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record RemoveItemFromCartCommand(string BuyerId, Guid ProductId) : IRequest<Unit>;

    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand,Unit>
    {
        private readonly IBasketDbContext _db;

        public RemoveItemFromCartCommandHandler(IBasketDbContext db) => _db = db;

        public async Task<Unit> Handle(RemoveItemFromCartCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                throw new InvalidOperationException("سبد خرید خالی می باشد !");

            cart.RemoveItem(request.ProductId);
            await _db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
