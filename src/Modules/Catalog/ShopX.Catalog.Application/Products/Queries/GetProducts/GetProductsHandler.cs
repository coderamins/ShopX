using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.Application.Products.DTOs;
using ShopX.Catalog.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Queries.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly CatalogDbContext _db;
        private readonly IMapper _mapper;

        public GetProductsHandler(CatalogDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper=mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery req, CancellationToken ct)
        {
            var products = await _db.Products
                .ToListAsync(ct);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

    }
}
