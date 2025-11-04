using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Domain.Entities;
using ShopX.Basket.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record AddItemToCartCommand(string BuyerId, Guid ProductId, int Quantity, decimal UnitPrice) : IRequest<Unit>;

}
