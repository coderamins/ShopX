using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Basket.Infrastructure
{
    public class ShoppingCartItemConfig : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductId).IsRequired();
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Quantity).IsRequired();
        }
    }
}
