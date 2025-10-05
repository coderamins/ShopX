using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Products.DTOs;
using ShopX.Catalog.Infrastructure.Persistence;

namespace ShopX.Catalog.Application.Products.Queries.GetShopingCartQuery
{
    public sealed class GetShopingCartQueryHandler:IRequestHandler<GetShopingCartQueryQuery,ProductDto>
    {
        private readonly Basket _db;
        private readonly IMapper _mapper;

        public GetShopingCartQueryHandler(CatalogDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ProductDto?> Handle(GetShopingCartQueryQuery query, CancellationToken ct)
        {
            // شبیه‌سازی تاخیر مصنوعی
            await Task.Delay(2000, ct); // ۲ ثانیه تاخیر

            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == ProductId.FromGuid(query.Id), ct);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }
    }
}
