using Shopx.Catalog.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Catalog.Infrastructure.Persistence
{
    public static class CatalogDbContextSeed
    {
        public static async Task SeedAsync(CatalogDbContext context)
        {
            if (!context.Products.Any())
            {
                List<Product> products = new List<Product>
                {
                    new("Laptop Dell XPS 15",3200m,10),
                    new("IPhone 15 Pro Max",1500m,25),
                    new("Sony WH-100XM5",400m,50),
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }

    }
}
