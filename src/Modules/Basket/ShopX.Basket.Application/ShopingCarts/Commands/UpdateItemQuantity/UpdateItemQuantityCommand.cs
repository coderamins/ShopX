using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShopX.Basket.Application.ShopingCarts.Commands.UpdateItemQuantity
{
    public record UpdateItemQuantityCommand(string BuyerId, Guid ProductId, int Quantity) : IRequest<Unit>;
}
