using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;

namespace ShopX.Catalog.Application.Common.Interfaces
{
    public interface ICatalogDbContext
    {
        public DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
