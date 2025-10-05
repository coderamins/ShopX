using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Application.Products.DTOs
{
    public record ShopingCartDto(Guid Id, string Name, decimal Price, int Stock);

}
