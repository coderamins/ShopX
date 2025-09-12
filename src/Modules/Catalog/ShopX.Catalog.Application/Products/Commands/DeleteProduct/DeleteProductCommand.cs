using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShopX.Catalog.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):IRequest<bool>;
}
