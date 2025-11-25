FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5166
EXPOSE 5168

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["microservices.UserAPI/microservices.UserAPI.csproj", "microservices.UserAPI/"]
RUN dotnet restore "./microservices.UserAPI/microservices.UserAPI.csproj"
COPY . .
WORKDIR "/src/microservices.UserAPI"
RUN dotnet build "./microservices.UserAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./microservices.UserAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "microservices.UserAPI.dll"]