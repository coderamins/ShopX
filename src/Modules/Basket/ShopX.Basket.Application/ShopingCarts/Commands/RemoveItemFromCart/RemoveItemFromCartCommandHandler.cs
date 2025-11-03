using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopX.Basket.Application.Common;

namespace ShopX.Basket.Application.ShopingCarts.Commands.RemoveItemFromCart
{
    public class RemoveItemFromCartCommandHandler : IRequestHandler<RemoveItemFromCartCommand, Unit>
    {
        private readonly IBasketDbContext _db;

        public RemoveItemFromCartCommandHandler(IBasketDbContext db) => _db = db;

        public async Task<Unit> Handle(RemoveItemFromCartCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                throw new InvalidOperationException("سبد خرید یافت نشد.");

            cart.RemoveItem(request.ProductId);
            await _db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
