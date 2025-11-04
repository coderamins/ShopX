using ShopX.Basket.Application.Common.Interfaces;
using ShopX.Basket.Application.DTOs;
using System.Net.Http.Json;

namespace ShopX.Basket.Infrastructure.Services
{
    public class CatalogService: ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid productId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"/api/v1/catalog/products/{productId}");
        }
    }
}
