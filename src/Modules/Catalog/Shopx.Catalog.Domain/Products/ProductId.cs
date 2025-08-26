using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopx.Catalog.Domain.Products
{
    public sealed class ProductId
    {
        public Guid Value { get; private set; }

        private ProductId(Guid value) => Value = value;

        public static ProductId New() => new(Guid.NewGuid());

        public static ProductId FromGuid(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }
}
