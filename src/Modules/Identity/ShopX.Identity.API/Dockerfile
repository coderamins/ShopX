FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

copy ShopX.sln ./
 
COPY src/Modules/Identity/ShopX.Identity.API/ ./Modules/Identity/ShopX.Identity.API/
COPY src/Modules/Identity/ShopX.Identity.Application/ ./Modules/Identity/ShopX.Identity.Application/
COPY src/Modules/Identity/ShopX.Identity.Infrastructure/ ./Modules/Identity/ShopX.Identity.Infrastructure/
COPY src/Modules/Identity/ShopX.Identity.Domain/ ./Modules/Identity/ShopX.Identity.Domain/

COPY src/BuildingBlocks/ShopX.BuildingBlocks.Application/ ./BuildingBlocks/ShopX.BuildingBlocks.Application/
COPY src/BuildingBlocks/ShopX.BuildingBlocks.Domain/ ./BuildingBlocks/ShopX.BuildingBlocks.Domain/
COPY src/BuildingBlocks/ShopX.BuildingBlocks.Infrastructure/ ./BuildingBlocks/ShopX.BuildingBlocks.Infrastructure/

RUN dotnet restore Modules/Identity/ShopX.Identity.API/ShopX.Identity.API.csproj

# publish API
WORKDIR /src/Modules/Identity/ShopX.Identity.API
RUN dotnet publish -c Release -o /app/publish

# ----------------
# Runtime image
# ----------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShopX.Identity.API.dll"]
