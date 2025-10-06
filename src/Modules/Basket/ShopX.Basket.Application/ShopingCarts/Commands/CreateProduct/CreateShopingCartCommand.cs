using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public record CreateShopingCartCommand(string Name, decimal Price, int Stock):IRequest<Guid>;

}
