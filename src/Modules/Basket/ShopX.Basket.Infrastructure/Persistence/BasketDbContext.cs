﻿using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Basket.Infrastructure.Persistence
{
    public class BasketDbContext:DbContext
    {
        public BasketDbContext(DbContextOptions<BasketDbContext> options):base(options) { }

        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();
    }
}
