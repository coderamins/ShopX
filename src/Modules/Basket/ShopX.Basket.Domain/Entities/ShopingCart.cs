using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Basket.Domain.Entities
{
    public class ShopingCart
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string BuyerId { get; private set; }
        public List<ShopingCartItem> Items { get; private set; }

        public ShopingCart(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddItem(Guid productId, string productName, decimal price, int quantity)
        {
            var existing = Items.FirstOrDefault(i => i.ProductId==productId);
            if (existing is null)
            {
                Items.Add(new ShopingCartItem(productId, productName, price, quantity));
            }
            else
            {
                existing.AddQuantity(quantity);
            }
        }

        public void Clear() => Items.Clear();

    }
}
