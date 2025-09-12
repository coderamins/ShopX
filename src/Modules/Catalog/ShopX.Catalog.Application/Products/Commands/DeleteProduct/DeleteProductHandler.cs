using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler:IRequestHandler<DeleteProductCommand,bool>
    {
        private readonly CatalogDbContext _db;

        public DeleteProductHandler(CatalogDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id.Value == request.Id, ct);
            if (product is null) return false;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
