FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# کپی فایل solution
COPY ShopX.sln ./

# کپی همه پروژه‌های مورد نیاز (بدون تست‌ها)
COPY src/Modules/Basket/ShopX.Basket.API/ ./Modules/Basket/ShopX.Basket.API/
COPY src/Modules/Basket/ShopX.Basket.Application/ ./Modules/Basket/ShopX.Basket.Application/
COPY src/Modules/Basket/ShopX.Basket.Infrastructure/ ./Modules/Basket/ShopX.Basket.Infrastructure/
COPY src/Modules/Basket/ShopX.Basket.Domain/ ./Modules/Basket/ShopX.Basket.Domain/

# restore فقط روی API (که وابستگی‌هاش resolve بشن)
RUN dotnet restore Modules/Basket/ShopX.Basket.API/ShopX.Basket.API.csproj

# publish API
WORKDIR /src/Modules/Basket/ShopX.Basket.API
RUN dotnet publish -c Release -o /app/publish

# ----------------
# Runtime image
# ----------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Basket.API.dll"]
