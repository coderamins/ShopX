using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Basket.Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string BuyerId { get; private set; }
        public List<ShoppingCartItem> Items { get; private set; } = new();

        private ShoppingCart() { } // برای EF Core

        public ShoppingCart(string buyerId)
        {
            BuyerId = buyerId ?? throw new ArgumentNullException(nameof(buyerId));
        }

        public void AddItem(Guid productId, decimal unitPrice, int quantity)
        {
            var existing = Items.FirstOrDefault(i => i.ProductId == productId);
            if (existing is null)
            {
                var newItem = new ShoppingCartItem(productId, unitPrice, quantity);
                Items.Add(newItem);
            }
            else
            {
                existing.AddQuantity(quantity);
            }
        }

        public void RemoveItem(Guid productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is not null)
            {
                Items.Remove(item);
            }
        }

        public void UpdateItemQuantity(Guid productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null)
                throw new InvalidOperationException("محصول در سبد خرید وجود ندارد.");

            if (quantity <= 0)
                Items.Remove(item);
            else
                item.UpdateQuantity(quantity);
        }

        public void Clear() => Items.Clear();
    }
}
