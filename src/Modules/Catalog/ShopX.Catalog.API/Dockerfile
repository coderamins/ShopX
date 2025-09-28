FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# کپی فقط solution و پروژه‌های لازم
COPY ShopX.sln ./

COPY src/Modules/Catalog/ShopX.Catalog.API/ ./Modules/Catalog/ShopX.Catalog.API/
COPY src/Modules/Catalog/ShopX.Catalog.Application/ ./Modules/Catalog/ShopX.Catalog.Application/
COPY src/Modules/Catalog/ShopX.Catalog.Infrastructure/ ./Modules/Catalog/ShopX.Catalog.Infrastructure/
COPY src/BuildingBlocks/ShopX.BuildingBlocks.Application/ ./BuildingBlocks/ShopX.BuildingBlocks.Application/

# restore فقط روی پروژه‌های مورد نیاز API
RUN dotnet restore Modules/Catalog/ShopX.Catalog.API/ShopX.Catalog.API.csproj

# publish پروژه API
WORKDIR /src/Modules/Catalog/ShopX.Catalog.API
RUN dotnet publish -c Release -o /app/publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Catalog.API.dll"]
