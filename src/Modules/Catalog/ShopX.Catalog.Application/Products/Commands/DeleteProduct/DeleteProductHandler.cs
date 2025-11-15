using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Common.Interfaces;

namespace ShopX.Catalog.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler:IRequestHandler<DeleteProductCommand,bool>
    {
        private readonly ICatalogDbContext _db;

        public DeleteProductHandler(ICatalogDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == new ProductId(request.Id), ct);
            if (product is null) return false;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
