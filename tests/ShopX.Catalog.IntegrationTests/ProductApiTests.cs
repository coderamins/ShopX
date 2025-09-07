using FluentAssertions;
using ShopX.Catalog.Application.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shopx.Catalog.IntegrationTests
{
    public class ProductApiTests:IClassFixture<CatalogApiFactory>
    {
        private readonly HttpClient _client;

        public ProductApiTests(CatalogApiFactory factory)
        {
            _client=factory.CreateClient();
        }

        [Fact]
        public async Task Create_And_Get_Product_Should_Work()
        {
            //Arrange
            var createRequest = new { Name = "Test Product", Price = 99.9m, Stock = 5 };

            //Act
            var createResponse=await _client.PostAsJsonAsync("/products",createRequest);

            createResponse.EnsureSuccessStatusCode();

            var created = await createResponse.Content.ReadFromJsonAsync<ProductDto>();

            //Assert
            created.Should().NotBeNull();
            created!.Name.Should().Be("Test Product");
            created.Price.Should().Be(9.99m);
        }
    }
}
