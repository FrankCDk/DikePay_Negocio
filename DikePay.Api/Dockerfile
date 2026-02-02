# Fase Base: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
# Exponemos los puertos internos del contenedor
EXPOSE 8080
EXPOSE 8081

# Fase Build: SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# PASO CRÍTICO: Copiar todos los archivos .csproj para el Restore
# Esto permite que Docker cachee las capas de NuGet incluso si cambias código
COPY ["DikePay.Api/DikePay.Api.csproj", "DikePay.Api/"]
COPY ["DikePay.Modules.Catalog.Infrastructure/DikePay.Modules.Catalog.Infrastructure.csproj", "DikePay.Modules.Catalog.Infrastructure/"]
COPY ["DikePay.Modules.Catalog.Application/DikePay.Modules.Catalog.Application.csproj", "DikePay.Modules.Catalog.Application/"]
COPY ["DikePay.Modules.Catalog.Domain/DikePay.Modules.Catalog.Domain.csproj", "DikePay.Modules.Catalog.Domain/"]
COPY ["DikePay.Modules.Catalog.Shared/DikePay.Modules.Catalog.Shared.csproj", "DikePay.Modules.Catalog.Shared/"]
COPY ["DikePay.Modules.Auth.Infrastructure/DikePay.Modules.Auth.Infrastructure.csproj", "DikePay.Modules.Auth.Infrastructure/"]
COPY ["DikePay.Modules.Auth.Application/DikePay.Modules.Auth.Application.csproj", "DikePay.Modules.Auth.Application/"]
COPY ["DikePay.Modules.Auth.Domain/DikePay.Modules.Auth.Domain.csproj", "DikePay.Modules.Auth.Domain/"]
COPY ["DikePay.Modules.Auth.Shared/DikePay.Modules.Auth.Shared.csproj", "DikePay.Modules.Auth.Shared/"]
COPY ["DikePay.Modules.Configuration.Infrastructure/DikePay.Modules.Configuration.Infrastructure.csproj", "DikePay.Modules.Configuration.Infrastructure/"]
COPY ["DikePay.Modules.Configuration.Application/DikePay.Modules.Configuration.Application.csproj", "DikePay.Modules.Configuration.Application/"]
COPY ["DikePay.Modules.Configuration.Domain/DikePay.Modules.Configuration.Domain.csproj", "DikePay.Modules.Configuration.Domain/"]
COPY ["DikePay.Modules.Configuration.Shared/DikePay.Modules.Configuration.Shared.csproj", "DikePay.Modules.Configuration.Shared/"]
COPY ["DikePay.Shared.Infrastructure/DikePay.Shared.Infrastructure.csproj", "DikePay.Shared.Infrastructure/"]

# Ejecutar restore de la API (esto restaurará todas las dependencias)
RUN dotnet restore "DikePay.Api/DikePay.Api.csproj"

# Copiar el resto del código
COPY . .
WORKDIR "/src/DikePay.Api"
RUN dotnet build "DikePay.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DikePay.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DikePay.Api.dll"]