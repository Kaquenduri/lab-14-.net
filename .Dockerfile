# Etapa 1: Compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copiar los proyectos
COPY . .

# Restaurar dependencias
RUN dotnet restore lab10.Api/lab10.Api.csproj

# Compilar y publicar
RUN dotnet publish lab10.Api/lab10.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Etapa 2: Ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Puerto de Render
ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "lab10.Api.dll"]
