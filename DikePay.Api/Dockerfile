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
COPY ["DikePay.Modules.Articulos/DikePay.Modules.Articulos.csproj", "DikePay.Modules.Articulos/"]
COPY ["DikePay.Modules.Auth/DikePay.Modules.Auth.csproj", "DikePay.Modules.Auth/"]
COPY ["DikePay.Modules.Configuration/DikePay.Modules.Configuration.csproj", "DikePay.Modules.Configuration/"]
COPY ["DikePay.Shared/DikePay.Shared.csproj", "DikePay.Shared/"]

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