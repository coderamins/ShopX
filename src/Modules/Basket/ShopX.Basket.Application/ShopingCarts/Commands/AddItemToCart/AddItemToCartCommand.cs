using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShopX.Basket.Application.ShopingCarts.Commands.AddItemToCart
{
    // Features/ShoppingCart/Commands/AddItemToCart/AddItemToCartCommand.cs
    public record AddItemToCartCommand(string BuyerId, Guid ProductId, int Quantity) : IRequest<Unit>;

}
