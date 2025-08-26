using ShopX.Catalog.Application.Products.Commands.CreateProduct;
using ShopX.Catalog.Application.Products.Queries.GetProductById;

namespace ShopX.Catalog.API.Endpoints
{
    public static class ProductsEndpoints
    {
        public static void MapCatalogEndpoints(this WebApplication app)
        {
            app.MapPost("/api/v1/catalog/products", async (CreateProductCommand cmd, CreateProductHandler handler) =>
            {
                var id = await handler.Handle(cmd, CancellationToken.None);
                return Results.Created($"/api/v1/catalog/products/{id}", new { id });
            });

            app.MapGet("/api/v1/catalog/products/{id:guid}", async (Guid id, GetProductByIdHandler handler) =>
            {
                var product = await handler.Handle(new GetProductByIdQuery(id), CancellationToken.None);
                return product is null ? Results.NotFound() : Results.Ok(product);
            });
        }
    }
}
