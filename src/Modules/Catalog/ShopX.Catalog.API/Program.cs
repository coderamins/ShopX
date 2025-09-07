using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.API.Endpoints;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;
using ShopX.Catalog.Infrastructure.Persistence;
using Mapster;
using ShopX.Catalog.Application.Products.Mappings;
using ShopX.Catalog.Application.Products.Queries.GetProductById;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ----Serilog---
Log.Logger=new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

Log.Warning("Test logging!");


builder.Host.UseSerilog();

// --- EF Core ---
builder.Services.AddDbContext<CatalogDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Mapster
builder.Services.AddSingleton(MapsterConfig.CreateMapper());

// Handlers
builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();

// --- HealthChecks ---
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Postgres")!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseSerilogRequestLogging();

// --- Run DB Migration & Seed ---
using (var scope=app.Services.CreateScope())
{
    var db=scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    db.Database.Migrate();  
    await CatalogDbContextSeed.SeedAsync(db);
}

// --- Map Endpoints
app.MapCatalogEndpoints();

// --- Health endpoint ---
app.MapHealthChecks("/health");

app.Run();

Console.WriteLine("Application is running...");

public partial class Program { }