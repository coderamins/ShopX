using MediatR;
using ShopX.Catalog.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IRequest<ProductDto>;

}
