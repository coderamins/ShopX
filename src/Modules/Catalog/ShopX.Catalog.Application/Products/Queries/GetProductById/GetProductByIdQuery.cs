using MediatR;
using ShopX.Catalog.Application.Products.DTOs;

namespace ShopX.Catalog.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IRequest<ProductDto>;

}
