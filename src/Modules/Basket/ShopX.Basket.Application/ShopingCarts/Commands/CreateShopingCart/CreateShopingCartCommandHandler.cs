using MediatR;
using ShopX.Basket.Application.Common.Interfaces;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record CreateShopingCartCommand(string BuyerId) : IRequest<Guid>;

    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShopingCartCommand, Guid>
    {
        private readonly IBasketDbContext _db;

        public CreateShoppingCartCommandHandler(IBasketDbContext db) => _db = db;

        public async Task<Guid> Handle(CreateShopingCartCommand request, CancellationToken ct)
        {
            var cart = new ShoppingCart(request.BuyerId);
            _db.ShoppingCarts.Add(cart);
            await _db.SaveChangesAsync(ct);
            return cart.Id;
        }
    }
}
