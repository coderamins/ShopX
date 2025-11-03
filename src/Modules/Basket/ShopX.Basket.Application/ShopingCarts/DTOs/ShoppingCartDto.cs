using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Basket.Application.ShopingCarts.DTOs
{
    public record ShoppingCartDto(string BuyerId, List<BasketItemDto> Items);
    public record BasketItemDto(Guid ProductId, int Quantity, decimal UnitPrice);


}
