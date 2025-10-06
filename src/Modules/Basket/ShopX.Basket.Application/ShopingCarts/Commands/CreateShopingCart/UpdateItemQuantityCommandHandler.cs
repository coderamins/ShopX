using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record UpdateItemQuantityCommand(string BuyerId, Guid ProductId, int Quantity) : IRequest;

    public class UpdateItemQuantityCommandHandler : IRequestHandler<UpdateItemQuantityCommand>
    {
        private readonly BasketDbContext _db;

        public UpdateItemQuantityCommandHandler(BasketDbContext db) => _db = db;

        public async Task<Unit> Handle(UpdateItemQuantityCommand request, CancellationToken ct)
        {
            var cart = await _db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId, ct);

            if (cart is null)
                throw new InvalidOperationException("سبد خرید خالی می باشد !");

            cart.UpdateItemQuantity(request.ProductId, request.Quantity);
            await _db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
