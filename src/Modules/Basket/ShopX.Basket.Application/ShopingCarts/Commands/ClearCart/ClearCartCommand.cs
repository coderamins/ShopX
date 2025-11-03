using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShopX.Basket.Application.ShopingCarts.Commands.ClearCart
{
    public record ClearCartCommand(string BuyerId) : IRequest<Unit>;
}
