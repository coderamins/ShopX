using MediatR;
using ShopX.Basket.Domain.Entities;
using ShopX.Basket.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record CreateShopingCartCommand(string BuyerId) : IRequest<Guid>;

    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShopingCartCommand, Guid>
    {
        private readonly BasketDbContext _db;

        public CreateShoppingCartCommandHandler(BasketDbContext db) => _db = db;

        public async Task<Guid> Handle(CreateShopingCartCommand request, CancellationToken ct)
        {
            var cart = new ShoppingCart(request.BuyerId);
            _db.ShoppingCarts.Add(cart);
            await _db.SaveChangesAsync(ct);
            return cart.Id;
        }
    }
}
