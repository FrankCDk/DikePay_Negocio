using DikePay.Modules.Articulos.Application.Abstractions.Persistence;
using DikePay.Modules.Articulos.Application.Features.v1.Handlers;
using DikePay.Modules.Articulos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Modules.Articulos.Infrastructure
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

            services.AddDbContext<ArticuloDbContext>(options =>
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
                cfg.RegisterServicesFromAssembly(typeof(CrearArticuloHandler).Assembly);
            });

            // 2. Registro de Repositorios y Unit of Work
            // Usamos Scoped para que vivan lo que dura la petición HTTP
            services.AddScoped<IArticuloRepository, ArticuloRepository>();
            services.AddScoped<IArticuloUnitOfWork, ArticuloUnitOfWork>();

            return services;
        }
    }
}
