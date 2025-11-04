using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Common.Interfaces;
using ShopX.Catalog.Application.Products.DTOs;

namespace ShopX.Catalog.Application.Products.Queries.GetProductById
{
    public sealed class GetProductByIdHandler:IRequestHandler<GetProductByIdQuery,ProductDto>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(ICatalogDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            // شبیه‌سازی تاخیر مصنوعی
            await Task.Delay(2000, ct); // ۲ ثانیه تاخیر

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId.FromGuid(query.Id), ct);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }
    }
}
