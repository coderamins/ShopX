using Microsoft.EntityFrameworkCore;
using ShopX.Basket.API.Endpoints;
using ShopX.Basket.Application;
using ShopX.Basket.Infrastructure;
using ShopX.Basket.Infrastructure.Persistence;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// --- EF Core ---
builder.Services.AddDbContext<BasketDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration
    .GetConnectionString("Postgres")).EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

// Mapster
//builder.Services.AddSingleton(MapsterConfig.CreateMapper());
builder.Services.AddMapping();

// Handlers
builder.Services.AddScoped<AddItemToCartCommandHandler>();
builder.Services.AddScoped<CreateShoppingCartCommandHandler>();
builder.Services.AddScoped<RemoveItemFromCartCommandHandler>();
builder.Services.AddScoped<UpdateItemQuantityCommandHandler>();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapShopingCartEndpoints();

app.Run();
