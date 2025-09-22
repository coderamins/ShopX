FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# سلوشن و csproj ها
COPY ShopX.sln ./
COPY src/Modules/Catalog/ShopX.Catalog.API/ShopX.Catalog.API.csproj src/Modules/Catalog/ShopX.Catalog.API/
COPY src/Modules/Catalog/ShopX.Catalog.Application/ShopX.Catalog.Application.csproj src/Modules/Catalog/ShopX.Catalog.Application/
COPY src/Modules/Catalog/ShopX.Catalog.Infrastructure/ShopX.Catalog.Infrastructure.csproj src/Modules/Catalog/ShopX.Catalog.Infrastructure/
COPY src/BuildingBlocks/ShopX.BuildingBlocks.Application/ShopX.BuildingBlocks.Application.csproj src/BuildingBlocks/ShopX.BuildingBlocks.Application/

# ریستور
RUN dotnet restore ShopX.sln

# کل سورس
COPY . .

WORKDIR /src/src/Modules/Catalog/ShopX.Catalog.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Catalog.API.dll"]
