using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Basket.Domain.Entities
{
    public class ShoppingCartItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private ShoppingCartItem() { } // EF Core

        public ShoppingCartItem(Guid productId, decimal price, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("تعداد باید بیشتر از صفر باشد.");

            ProductId = productId;
            UnitPrice = price;
            Quantity = quantity;
        }

        public void AddQuantity(int quantity) => Quantity += quantity;

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("تعداد باید بیشتر از صفر باشد.");
            Quantity = quantity;
        }
    }
}
