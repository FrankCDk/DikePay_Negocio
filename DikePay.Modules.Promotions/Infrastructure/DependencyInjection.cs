using DikePay.Modules.Promotions.Application.Abstractions.Persistence;
using DikePay.Modules.Promotions.Application.Features.v1.Queries;
using DikePay.Modules.Promotions.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DikePay.Modules.Promotions.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPromotionsModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            services.AddDbContext<PromotionsDbContext>(options =>
                options.UseMySql(connectionString, serverVersion, mysqlOptions =>
                {
                    // Recomendado para apps robustas
                    mysqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                })
                //.ReplaceService<IValueConverterSelector, ValueConverterSelector>()
            );

            // Registramos MediatR especificando el ensamblado de este módulo
            services.AddMediatR(cfg =>
            {
                // Escanea los Handlers solo de la capa Application de Catalog
                cfg.RegisterServicesFromAssembly(typeof(GetAllPromotionsQueryHandler).Assembly);
            });

            services.AddScoped<IPromotionsRepository, PromotionsRepository>();
            services.AddScoped<IPromotionsUnitOfWork, PromotionsUnitOfWork>();

            return services;
        }

    }
}
