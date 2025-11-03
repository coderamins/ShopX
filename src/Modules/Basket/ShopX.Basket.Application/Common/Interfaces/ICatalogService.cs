using ShopX.Basket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Basket.Application.Common.Interfaces
{
    public interface ICatalogService
    {
        Task<ProductDto?> GetProductByIdAsync(Guid productId);
    }
}
