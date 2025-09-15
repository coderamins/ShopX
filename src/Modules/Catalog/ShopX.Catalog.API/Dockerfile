# „—Õ·Â build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "ShopX.Catalog.API.csproj"
RUN dotnet publish "ShopX.Catalog.API.csproj" -c Release -o /app/publish

# „—Õ·Â runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Catalog.API.dll"]
