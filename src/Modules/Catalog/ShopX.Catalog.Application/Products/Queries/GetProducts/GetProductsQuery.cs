using MediatR;
using ShopX.Catalog.Application.Products.DTOs;

namespace ShopX.Catalog.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery():IRequest<IEnumerable<ProductDto>>;
}
