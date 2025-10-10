using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopX.Basket.Application.ShopingCarts.DTOs;
using ShopX.Basket.Infrastructure.Persistence;

namespace ShopX.Basket.Application.Queries;

public sealed class GetBasketByBuyerIdHandler
    : IRequestHandler<GetBasketByBuyerIdQuery, ShoppingCartDto?>
{
    private readonly BasketDbContext _db;

    public GetBasketByBuyerIdHandler(BasketDbContext db)
    {
        _db = db;
    }

    public async Task<ShoppingCartDto?> Handle(GetBasketByBuyerIdQuery request, CancellationToken ct)
    {
        var cart = await _db.ShoppingCarts
            .Include(c => c.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.BuyerId == request.BuyerId.ToString(), ct);

        return cart?.Adapt<ShoppingCartDto>();
    }
}
