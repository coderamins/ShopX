using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ShopX.Basket.Application.Common;

namespace ShopX.Basket.Application.ShopingCarts.Commands.ClearCart
{
    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, Unit>
    {
        private readonly IBasketDbContext _db;

        public ClearCartCommandHandler(IBasketDbContext db) => _db = db;

        public async Task<Unit> Handle(ClearCartCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                return Unit.Value;

            cart.Clear();
            await _db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
