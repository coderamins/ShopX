using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopX.BuildingBlocks.Domain
{
    /// <summary>
    /// همه رخدادهای دامنه باید این قرارداد رو پیاده‌سازی کنن.
    /// اینترفیس خیلی سبک هست تا Aggregate Rootها بتونن رخدادهاشون رو جمع‌آوری کنن.
    /// </summary>
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
