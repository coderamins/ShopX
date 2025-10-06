using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.DTOs
{
    public record ShoppingCartDto(Guid Id, string BuyerId, List<BasketItemDto> Items);
    public record BasketItemDto(Guid ProductId, int Quantity, decimal UnitPrice);


}
