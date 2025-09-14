using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Infrastructure.Persistence
{
    public sealed class CatalogDbContext : DbContext
    {
        public const string Schema = "catalog";

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Id)
                    .HasConversion(
                        id => id.Value,
                        value => new ProductId(value));
            });
        }
    }
}
