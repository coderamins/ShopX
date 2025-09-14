using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Products.DTOs;
using ShopX.Catalog.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Queries.GetProductById
{
    public sealed class GetProductByIdHandler:IRequestHandler<GetProductByIdQuery,ProductDto>
    {
        private readonly CatalogDbContext _db;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(CatalogDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId.FromGuid(query.Id), ct);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }
    }
}
