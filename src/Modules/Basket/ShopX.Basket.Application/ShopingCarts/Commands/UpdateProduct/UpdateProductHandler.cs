using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler:IRequestHandler<UpdateProductCommand,bool>
    {
        private readonly CatalogDbContext _db;

        public UpdateProductHandler(CatalogDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(UpdateProductCommand request,CancellationToken ct)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == new ProductId(request.Id), ct);
            if (product is null) return false;

            product.Update(request.Name, request.Price, request.Stock);
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
