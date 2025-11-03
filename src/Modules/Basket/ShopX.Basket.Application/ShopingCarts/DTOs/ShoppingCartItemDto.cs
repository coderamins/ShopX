namespace ShopX.Basket.Application.DTOs;

public record ShoppingCartItemDto(Guid ProductId, /*string ProductName, */int Quantity, decimal UnitPrice);
