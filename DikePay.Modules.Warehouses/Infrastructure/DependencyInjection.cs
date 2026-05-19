using DikePay.Modules.Warehouses.Infrastructure.Persistence;
using DikePay.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Modules.Warehouses.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWarehousesInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddModuleDbContext<WarehouseDbContext>(configuration);
            
            return services;

        }
    }
}
