using MediatR;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record AddItemToCartCommand(string BuyerId, Guid ProductId, int Quantity, decimal UnitPrice) : IRequest<Unit>;

}
