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
        public List<ShoppingCartItem> Items { get; private set; }

        public ShoppingCart(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddItem(Guid productId, decimal price, int quantity)
        {
            var existing = Items.FirstOrDefault(i => i.ProductId==productId);
            if (existing is null)
            {
                Items.Add(new ShoppingCartItem(productId, price, quantity));
            }
            else
            {
                existing.AddQuantity(quantity);
            }
        }

        public void Clear() => Items.Clear();

        public void RemoveItem(Guid productId)
        {
            var existing = Items.FirstOrDefault(i => i.ProductId==productId);
            if (existing is not null)
            {
                Items.Remove(existing);
            }
        }

        public void UpdateItemQuantity(Guid productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null)
                throw new InvalidOperationException("محصول در سبد خرید وجود ندارد.");

            if (quantity <= 0)
            {
                // اگر تعداد 0 یا منفی باشه، آیتم حذف بشه
                Items.Remove(item);
            }
            else
            {
                item.UpdateQuantity(quantity);
            }
        }
    }
}
