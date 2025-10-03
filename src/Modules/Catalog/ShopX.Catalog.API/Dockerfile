FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# کپی فایل solution
COPY ShopX.sln ./

# کپی همه پروژه‌های مورد نیاز (بدون تست‌ها)
COPY src/Modules/Catalog/ShopX.Catalog.API/ ./Modules/Catalog/ShopX.Catalog.API/
COPY src/Modules/Catalog/ShopX.Catalog.Application/ ./Modules/Catalog/ShopX.Catalog.Application/
COPY src/Modules/Catalog/ShopX.Catalog.Infrastructure/ ./Modules/Catalog/ShopX.Catalog.Infrastructure/
COPY src/Modules/Catalog/Shopx.Catalog.Domain/ ./Modules/Catalog/Shopx.Catalog.Domain/

COPY src/BuildingBlocks/ShopX.BuildingBlocks.Application/ ./BuildingBlocks/ShopX.BuildingBlocks.Application/
COPY src/BuildingBlocks/ShopX.BuildingBlocks.Domain/ ./BuildingBlocks/ShopX.BuildingBlocks.Domain/
COPY src/BuildingBlocks/ShopX.BuildingBlocks.Infrastructure/ ./BuildingBlocks/ShopX.BuildingBlocks.Infrastructure/

# restore فقط روی API (که وابستگی‌هاش resolve بشن)
RUN dotnet restore Modules/Catalog/ShopX.Catalog.API/ShopX.Catalog.API.csproj

# publish API
WORKDIR /src/Modules/Catalog/ShopX.Catalog.API
RUN dotnet publish -c Release -o /app/publish

# ----------------
# Runtime image
# ----------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Catalog.API.dll"]
