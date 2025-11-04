using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Application.Common.ShopX.Basket.Application.Common.Interfaces;

namespace ShopX.Basket.Application.ShopingCarts.Commands.UpdateItemQuantity
{
    public class UpdateItemQuantityCommandHandler : IRequestHandler<UpdateItemQuantityCommand, Unit>
    {
        private readonly IBasketDbContext _db;

        public UpdateItemQuantityCommandHandler(IBasketDbContext db) => _db = db;

        public async Task<Unit> Handle(UpdateItemQuantityCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                throw new InvalidOperationException("سبد خرید یافت نشد.");

            cart.UpdateItemQuantity(request.ProductId, request.Quantity);
            await _db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
