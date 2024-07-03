using AutoMapper;
using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MinimalAPIERP.Dtos;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERP.Api;

internal static class ProductApi
{
    public static RouteGroupBuilder MapProductApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Product Api");

        group.MapGet("/product/{Guid}", async Task<Results<Ok<ProductViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Guid == guid);
            return product != null ? TypedResults.Ok(mapper.Map<ProductViewDto>(product)) : TypedResults.NotFound();
        })
       .WithOpenApi();

        group.MapGet("/product/all", async Task<Results<Ok<IList<ProductViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<Product> products = await db.Products
                .Include(x => x.Category)
                .ToListAsync();
            return products.Any() ? TypedResults.Ok(mapper.Map<IList<ProductViewDto>>(products)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/product/totalPages", async Task<Ok<int>> (AppDbContext db, IMapper mapper, int pageSize = 10) =>
        {
            int totalPages = (int) Math.Ceiling(await db.Products.CountAsync() / (float) pageSize) -1;
            return TypedResults.Ok(totalPages);
        })
        .WithOpenApi();

        group.MapGet("/product/paged", async Task<Results<Ok<IList<ProductViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<Product> products = await db.Products
                .OrderBy(x => x.Guid)
                .Include(x => x.Category)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return products.Any() ? TypedResults.Ok(mapper.Map<IList<ProductViewDto>>(products)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/product", async Task<Results<Created<ProductViewDto>, BadRequest, NotFound>> (ProductDto productDto, AppDbContext db, IMapper mapper) =>
        {
            Category? category = await db.Categories.FirstOrDefaultAsync(x => x.Guid == productDto.CategoryGuid);
            if (category is null)
            {
                return TypedResults.NotFound();
            }

            Product? product = mapper.Map<Product>(productDto);
            product.Category = category;

            db.Products.Add(product);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/product/{product.Guid}", mapper.Map<ProductViewDto>(product));
        })
        .WithOpenApi();

        group.MapPut("/product/{Guid}", async Task<Results<Ok<ProductViewDto>, NotFound, BadRequest>> (Guid guid, ProductDto productDto, AppDbContext db, IMapper mapper) =>
        {
            Category? category = await db.Categories.FirstOrDefaultAsync(x => x.Guid == productDto.CategoryGuid);
            if (category is null)
            {
                return TypedResults.NotFound();
            }

            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == guid);
            if (product == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(productDto, product);
            product.Category = category;

            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<ProductViewDto>(product));
        })
        .WithOpenApi();

        group.MapDelete("/product/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == guid);
            if (product == null)
            {
                return TypedResults.NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();

        return group;
    }
}
