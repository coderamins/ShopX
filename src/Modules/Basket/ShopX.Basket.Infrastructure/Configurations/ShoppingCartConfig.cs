using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Domain.Entities;

namespace ShopX.Basket.Infrastructure
{
    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.BuyerId)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
