using DikePay.Modules.Configuration.Domain.Interfaces;
using DikePay.Modules.Configuration.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace DikePay.Modules.Configuration.Infrastructure
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
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });

            services.AddAutoMapper(cfg => 
                    cfg.AddMaps(Application.AssemblyReference.Assembly)
                );
            services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IConfigurationUnitOfWork, ConfigurationUnitOfWork>();

            return services;
        }
    }
}
