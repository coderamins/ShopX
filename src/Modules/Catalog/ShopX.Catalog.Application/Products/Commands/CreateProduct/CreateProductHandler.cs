using MapsterMapper;
using MediatR;
using Shopx.Catalog.Domain.Products;
using ShopX.Catalog.Application.Common.Interfaces;

namespace ShopX.Catalog.Application.Products.Commands.CreateProduct
{
    public sealed class CreateProductHandler:IRequestHandler<CreateProductCommand,Guid>
    {
        private readonly ICatalogDbContext _db;
        private readonly IMapper _mapper;

        public CreateProductHandler(ICatalogDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand cmd, CancellationToken ct)
        {
            var product = new Product(cmd.Name, cmd.Price, cmd.Stock);
            await _db.Products.AddAsync(product, ct);
            await _db.SaveChangesAsync(ct);
            return product.Id.Value;
        }
    }
}
