using Shopx.Catalog.Domain.Products.Events;
using ShopX.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopx.Catalog.Domain.Products
{
    public sealed class Product
    {
        public ProductId Id { get; private set; } = ProductId.New();
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _events.AsReadOnly();

        private Product() { } // EF

        public Product(string name, decimal price, int stock)
        {
            Id = ProductId.New();
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void Update(string name, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));
            if (price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(price));
            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative", nameof(stock));

            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (Price == newPrice) return;
            Price = newPrice;
            _events.Add(new ProductPriceChanged(Id, newPrice));
        }
    }
}
