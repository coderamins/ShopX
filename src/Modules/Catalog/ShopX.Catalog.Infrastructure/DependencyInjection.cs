using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ShopX.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.Load("ShopX.Catalog.Application")));

            return services;
        }
    }
}
