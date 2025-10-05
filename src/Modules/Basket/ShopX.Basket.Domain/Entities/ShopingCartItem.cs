using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.Basket.Domain.Entities
{
    public class ShopingCartItem
    {
        public Guid Id { get; private set; }=Guid.NewGuid();
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }   

        private ShopingCartItem() { }

        public ShopingCartItem(Guid productId,string productName,decimal price,int quantity)
        {
            ProductId=productId;
            ProductName=productName;
            Price=price;
            Quantity=quantity;
        }

        public void AddQuantity(int quantity) => Quantity+=quantity;

    }
}
