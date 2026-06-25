using DikePay.Modules.Configuracion.Application.Features.v1.Versions.Handlers;
using DikePay.Modules.Configuracion.Domain.Interfaces;
using DikePay.Modules.Configuracion.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Modules.Configuracion.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigurationModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<ConfigurationDbContext>(options =>
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
                cfg.RegisterServicesFromAssembly(typeof(CrearVersionHandler).Assembly);
            });

            services.AddAutoMapper(cfg =>
                    cfg.AddMaps(typeof(CrearVersionHandler).Assembly)
                );
            services.AddScoped<IConfiguracionRepository, ConfigurationRepository>();
            services.AddScoped<IConfiguracionUnitOfWork, ConfigurationUnitOfWork>();

            return services;
        }
    }
}
