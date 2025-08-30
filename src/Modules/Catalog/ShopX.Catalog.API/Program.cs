using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.API.Endpoints;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;
using ShopX.Catalog.Infrastructure.Persistence;
using Mapster;
using ShopX.Catalog.Application.Products.Mappings;
using ShopX.Catalog.Application.Products.Queries.GetProductById;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddSingleton(MapsterConfig.CreateMapper());

// Handlers
builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();

var app = builder.Build();
app.MapCatalogEndpoints();
app.Run();