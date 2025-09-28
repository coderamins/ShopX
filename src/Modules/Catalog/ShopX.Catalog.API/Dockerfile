FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# کپی solution و کل محتویات پروژه (بدون اضافه کردن src دوم)
COPY . .

# restore
RUN dotnet restore ShopX.sln

# publish
WORKDIR /src/Modules/Catalog/ShopX.Catalog.API
RUN dotnet publish -c Release -o /app/publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Catalog.API.dll"]
