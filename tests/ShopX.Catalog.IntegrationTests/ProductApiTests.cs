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
    public class ProductApiTests : IClassFixture<CatalogApiFactory>
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
            var createResponse = await _client.PostAsJsonAsync("/api/v1/catalog/products", createRequest);
            createResponse.EnsureSuccessStatusCode();

            var created = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
            created.Should().NotBeNull();

            // Act → Get Product by Id
            var getResponse = await _client.GetAsync($"/api/v1/catalog/products/{created.Id}");
            getResponse.EnsureSuccessStatusCode();

            var product = await getResponse.Content.ReadFromJsonAsync<ProductDto>();

            //Assert
            product.Should().NotBeNull();
            product.Id!.Should().Be(created.Id);
            product!.Name.Should().Be("Test Product");
            product.Price.Should().Be(99.9m);
            product.Stock.Should().Be(5);
        }

        //[Fact]
        //public async Task GetAllProducts_Should_Return_List()
        //{
        //    //Arrange
        //    var createRequests = new[]
        //    {
        //         new {Name = "Product 1", Price = 10m, Stock = 5},
        //         new { Name = "Product 2", Price = 20m, Stock = 3 }
        //    };

        //    foreach (var req in createRequests)
        //        await _client.PostAsJsonAsync("/api/v1/catalog/products", req);

        //    //Act
        //    var response = await _client.GetAsync("/api/v1/catalog/products4");
        //    response.EnsureSuccessStatusCode();

        //    var products=await response.Content.ReadFromJsonAsync<List<ProductDto>>();

        //    //Assert
        //    products.Should().NotBeNull();
        //    products.Count.Should().BeGreaterThanOrEqualTo(2);
        //    products.Select(p => p.Name).Should().Contain(new[] { "Product 1", "Product 2" });
        //}

        //[Fact]
        //public async Task Update_Product_Should_Change_Values()
        //{
        //    //Arrange
        //    var createResponse=await _client.PostAsJsonAsync("/api/v1/catalog/products",
        //        new { Name = "Old Name", Price = 5m, Stock = 2 });

        //    var created = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
        //    var productId = created!.Id;

        //    //Act
        //    var updateRequest= new { Name = "New Name", Price = 15m, Stock = 10 };
        //    var updateResponse = await _client.PutAsJsonAsync($"/api/v1/catalog/products/{productId}", updateRequest);
        //    updateResponse.EnsureSuccessStatusCode();

        //    // Assert
        //    var getResponse = await _client.GetAsync($"/api/v1/catalog/products/{productId}");
        //    var updated = await getResponse.Content.ReadFromJsonAsync<ProductDto>();

        //    updated!.Name.Should().Be("New Name");
        //    updated.Price.Should().Be(15m);
        //    updated.Stock.Should().Be(10);
        //}

        //[Fact]
        //public async Task Delete_Product_Should_Remove_It()
        //{
        //    //Arrange
        //    var createResponse = await _client.PostAsJsonAsync("/api/v1/catalog/products/",
        //       new { Name = "To be Deleted", Price = 5m, Stock = 2 });
        //    var created = await createResponse.Content.ReadFromJsonAsync<CreateProductResponse>();
        //    var productId = created!.Id;

        //    //Act
        //    var deletedResponse = await _client.DeleteAsync($"/api/v1/catalog/products/{productId}");
        //    deletedResponse.EnsureSuccessStatusCode();

        //    //Assert
        //    var getResponse = await _client.GetAsync($"/api/v1/catalog/products/{productId}");
        //    getResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        //}
    }
}
