using DikePay.Modules.Almacenes.Infrastructure.Persistence;
using DikePay.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Modules.Almacenes.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWarehousesInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddModuleDbContext<AlmacenDbContext>(configuration);
            
            return services;

        }
    }
}
