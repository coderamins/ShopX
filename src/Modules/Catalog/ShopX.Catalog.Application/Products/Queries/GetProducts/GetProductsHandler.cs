using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.Application.Products.DTOs;
using ShopX.Catalog.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Queries.GetProducts
{
    public class GetProductsHandler:IRequestHandler<GetProductsQuery,IEnumerable<ProductDto>>
    {
        private readonly CatalogDbContext _db;
        public GetProductsHandler(CatalogDbContext db) {  _db = db; }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery req, CancellationToken ct)
        {
            return await _db.Products
                .ProjectToType<ProductDto>()
                .ToListAsync(ct);
        }

    }
}
