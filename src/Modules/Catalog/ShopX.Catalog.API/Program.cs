using Microsoft.EntityFrameworkCore;
using ShopX.Catalog.API.Endpoints;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;
using ShopX.Catalog.Infrastructure.Persistence;
using Mapster;
using ShopX.Catalog.Application.Products.Mappings;
using ShopX.Catalog.Application.Products.Queries.GetProductById;
using Serilog;
using ShopX.Catalog.Infrastructure;
using ShopX.Catalog.Application;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Postgres: " + builder.Configuration.GetConnectionString("Postgres"));

// ----Serilog---
Log.Logger=new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

//---Observability---
var serviceName = "ShopX.Catalog";
var serviceVersion = "1.0.0";

builder.Services.AddOpenTelemetry()
    .ConfigureResource(r =>
        r.AddService(serviceName: serviceName, serviceVersion: serviceVersion))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSqlClientInstrumentation(opt =>
            {
                opt.SetDbStatementForText = true;
                opt.RecordException = true;
            })
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("http://otel-collector:4317"); // به collector می‌فرسته
            });
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            //.AddProcessInstrumentation()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri("http://otel-collector:4317");
            });
    });

builder.Host.UseSerilog((ctx, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration));

// --- EF Core ---
builder.Services.AddDbContext<CatalogDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Mapster
//builder.Services.AddSingleton(MapsterConfig.CreateMapper());
builder.Services.AddMapping();

// Handlers
builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();

// --- HealthChecks ---
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Postgres")!);

builder.Services.AddCatalogModule();
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

Log.Information("OpenTelemetry initialized for ShopX.Catalog");

app.Run();

Console.WriteLine("Application is running...");

public partial class Program { }