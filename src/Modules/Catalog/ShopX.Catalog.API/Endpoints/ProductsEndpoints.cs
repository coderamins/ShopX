using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopX.Catalog.Application.Products.Commands.CreateProduct;
using ShopX.Catalog.Application.Products.Commands.DeleteProduct;
using ShopX.Catalog.Application.Products.Commands.UpdateProduct;
using ShopX.Catalog.Application.Products.Queries.GetProductById;
using ShopX.Catalog.Application.Products.Queries.GetProducts;

namespace ShopX.Catalog.API.Endpoints
{
    public static class ProductsEndpoints
    {
        public static void MapCatalogEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/v1/catalog/products");

            // ایجاد محصول جدید
            group.MapPost("/", async ([FromBody] CreateProductCommand cmd, ISender sender) =>
            {
                var id = await sender.Send(cmd);
                return Results.Created($"/api/v1/catalog/products/{id}", new { id });
            });

            // دریافت محصول بر اساس شناسه
            group.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var product = await sender.Send(new GetProductByIdQuery(id));
                return product is null ? Results.NotFound() : Results.Ok(product);
            });

            // دریافت همه محصولات
            group.MapGet("/", async (ISender sender) =>
            {
                var products = await sender.Send(new GetProductsQuery());
                return Results.Ok(products);
            });

            // بروزرسانی محصول
            group.MapPut("/{id:guid}", async (Guid id, [FromBody] UpdateProductCommand cmd, ISender sender) =>
            {
                var updated = await sender.Send(cmd with { Id = id });
                return updated ? Results.NoContent() : Results.NotFound();
            });

            // حذف محصول
            group.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var deleted = await sender.Send(new DeleteProductCommand(id));
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
