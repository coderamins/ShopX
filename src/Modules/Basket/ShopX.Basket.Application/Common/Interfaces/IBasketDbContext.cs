using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Basket.Application.Common.Interfaces
{
    public interface IBasketDbContext
    {
        DbSet<ShoppingCart> ShoppingCarts { get; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

}
