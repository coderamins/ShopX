using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShopX.Basket.Application.ShopingCarts.Commands.RemoveItemFromCart
{
    public record RemoveItemFromCartCommand(string BuyerId, Guid ProductId) : IRequest<Unit>;
}
