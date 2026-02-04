using DikePay.Modules.Auth.Application.Abstractions.Interfaces;
using DikePay.Modules.Auth.Application.Abstractions.Persistence;
using DikePay.Modules.Auth.Application.Services;
using DikePay.Modules.Auth.Infrastructure.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace DikePay.Modules.Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            // 1. Configuración de la Base de Datos (MySQL con Pomelo)
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<AuthDbContext>(options =>
                options.UseMySql(connectionString, serverVersion, mysqlOptions =>
                {
                    // Recomendado para apps robustas
                    mysqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

            services.AddMediatR(cfg =>
            {
                // Escanea los Handlers solo de la capa Application de Catalog
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });

            services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

            // Aquí iría la configuración específica del módulo Auth
            // Por ejemplo, configuración de la base de datos, repositorios, servicios, etc.
            services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
