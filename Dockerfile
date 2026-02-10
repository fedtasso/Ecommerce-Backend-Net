# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiamos solución y proyectos
COPY *.sln .
COPY Ecommerce.Api/*.csproj Ecommerce.Api/
COPY Ecommerce.Application/*.csproj Ecommerce.Application/
COPY Ecommerce.Domain/*.csproj Ecommerce.Domain/
COPY Ecommerce.Infrastructure/*.csproj Ecommerce.Infrastructure/
COPY Ecommerce.Shared/*.csproj Ecommerce.Shared/

# Restore
RUN dotnet restore

# Copiamos el resto del código
COPY . .

# Publicamos la API
RUN dotnet publish Ecommerce.Api/Ecommerce.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

# Render usa SIEMPRE el puerto 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]
