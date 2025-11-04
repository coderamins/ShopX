using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.Application.Common.Interfaces;
using ShopX.Catalog.Application.Products.DTOs;

namespace ShopX.Catalog.Application.Products.Queries.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public GetProductsHandler(ICatalogDbContext db, IMapper mapper)
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
