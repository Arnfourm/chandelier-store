FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
# COPY ["microservices.CatalogAPI/microservices.CatalogAPI.csproj", "microservices.CatalogAPI/"]
COPY ["/microservices.CatalogAPI.csproj", "microservices.CatalogAPI/"]
RUN dotnet restore "./microservices.CatalogAPI/microservices.CatalogAPI.csproj"
COPY . .
WORKDIR "/src/microservices.CatalogAPI"
RUN dotnet build "./microservices.CatalogAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./microservices.CatalogAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microservices.CatalogAPI.dll"]