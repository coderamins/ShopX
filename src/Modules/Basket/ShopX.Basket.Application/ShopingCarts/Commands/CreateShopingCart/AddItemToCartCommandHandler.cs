using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record AddItemToCartCommand(string BuyerId, Guid ProductId, int Quantity, decimal UnitPrice) : IRequest<Unit>;

    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand,Unit>
    {
        private readonly BasketDbContext _db;

        public AddItemToCartCommandHandler(BasketDbContext db) => _db = db;

        public async Task<Unit> Handle(AddItemToCartCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                throw new InvalidOperationException("سبد خرید خالی می باشد !");

            cart.AddItem(request.ProductId, request.UnitPrice,request.Quantity);
            await _db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
