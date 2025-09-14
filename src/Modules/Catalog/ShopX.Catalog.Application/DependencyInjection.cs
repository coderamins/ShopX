using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using ShopX.Catalog.Application.Products.Mappings;
using System.Reflection;

namespace ShopX.Catalog.Application
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
