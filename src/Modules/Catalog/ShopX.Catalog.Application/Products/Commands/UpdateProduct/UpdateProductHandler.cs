using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Common.Interfaces;

namespace ShopX.Catalog.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler:IRequestHandler<UpdateProductCommand,bool>
    {
        private readonly ICatalogDbContext _db;

        public UpdateProductHandler(ICatalogDbContext db)
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
