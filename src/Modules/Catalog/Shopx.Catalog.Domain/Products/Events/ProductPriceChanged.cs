using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopx.Catalog.Domain.Products.Events
{
    public record ProductPriceChanged(ProductId ProductId, decimal NewPrice) : IDomainEvent;
}
