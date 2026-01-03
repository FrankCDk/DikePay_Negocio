using DikePay.Modules.Catalog.Application.Abstractions.Persistence;
using DikePay.Modules.Catalog.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Modules.Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCatalogModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. Configuración de la Base de Datos (MySQL con Pomelo)
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<CatalogDbContext>(options =>
                options.UseMySql(connectionString, serverVersion, mysqlOptions =>
                {
                    // Recomendado para apps robustas
                    mysqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));


            // Registramos MediatR especificando el ensamblado de este módulo
            services.AddMediatR(cfg =>
            {
                // Escanea los Handlers solo de la capa Application de Catalog
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });

            services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

            // 2. Registro de Repositorios y Unit of Work
            // Usamos Scoped para que vivan lo que dura la petición HTTP
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();

            return services;
        }
    }
}
