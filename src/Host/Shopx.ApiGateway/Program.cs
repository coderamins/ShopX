var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

// OpenTelemetry (حداقل وب + EF + HTTP)
builder.Services.AddOpenTelemetry()
    .WithTracing(b => b.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation())
    .WithMetrics(b => b.AddAspNetCoreInstrumentation());

// Swagger + Versioning
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(opt => { opt.DefaultApiVersion = new(1, 0); opt.AssumeDefaultVersionWhenUnspecified = true; });

// DbContexts
builder.Services.AddDbContext<CatalogDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

// Mapster
builder.Services.AddSingleton(MapsterConfig.CreateMapper());

// Redis (اختیاری)
builder.Services.AddStackExchangeRedisCache(o => o.Configuration = builder.Configuration["Redis:ConnectionString"]);

// Hangfire
builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/health");

// Mount کردن APIهای ماژول‌ها (روش ساده)
app.MapCatalogEndpoints();   // متد اکستنشن در ShopX.Catalog.API
app.MapIdentityEndpoints();  // ...

app.Run();
