using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ShopX.Basket.Application.ShopingCarts.Mappings;

namespace ShopX.Basket.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            // ثبت Global Config
            var config = MapsterConfig.CreateMapper();
            services.AddSingleton(config);

            // رجیستر IMapper که از این config استفاده کنه
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
