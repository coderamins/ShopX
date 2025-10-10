using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ShopX.Basket.Application.ShopingCarts.DTOs;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;

namespace ShopX.Basket.IntegrationTests
{
    public class BasketApiTests:IClassFixture<BasketApiFactory>
    {
        private readonly HttpClient _client;

        public BasketApiTests(BasketApiFactory factory)
        {
            _client = factory.CreateClient();   
        }

        [Fact]
        public async Task AddItem_And_GetBasket_Should_Work()
        {
            var buyerId=Guid.NewGuid();
            var productId=Guid.NewGuid();   

            var addCommand = new AddItemToCartCommand(buyerId.ToString(), productId, 1, 1000m);

            var addResponse = await _client.PostAsJsonAsync("/api/v1/basket/items", addCommand);
            addResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdCart=await addResponse.Content.ReadFromJsonAsync<ShoppingCartDto>();
            createdCart.Should().NotBeNull();
            createdCart!.Items.Should().ContainSingle(i => i.ProductId == productId);

            var getResponse = await _client.GetAsync($"/api/v1/basket/{buyerId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var cart=await getResponse.Content.ReadFromJsonAsync<ShoppingCartDto>();
            cart.Should().NotBeNull();
            cart!.Items.Should().HaveCount(1);
            cart.Items.First().ProductId.Should().Be(productId);

        }
    }
}
