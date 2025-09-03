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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}


using (var scope=app.Services.CreateScope())
{
    var db=scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    db.Database.Migrate();  
    await CatalogDbContextSeed.SeedAsync(db);
}

app.MapCatalogEndpoints();
app.Run();