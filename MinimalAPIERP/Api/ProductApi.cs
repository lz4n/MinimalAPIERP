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

        MapOrderEndpoints(group);
        MapCategoryEndpoints(group);

        return group;
    }
    private static void MapOrderEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("/order/{Guid}", async Task<Results<Ok<OrderViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            Order? order = await db.Orders.FirstOrDefaultAsync(x => x.Guid == guid);
            return order != null ? TypedResults.Ok(mapper.Map<OrderViewDto>(order)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/order/all", async Task<Results<Ok<IList<OrderViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<Order> orders = await db.Orders.ToListAsync();
            return orders.Any() ? TypedResults.Ok(mapper.Map<IList<OrderViewDto>>(orders)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/order/paged", async Task<Results<Ok<IList<OrderViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<Order> orders = await db.Orders
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return orders.Any() ? TypedResults.Ok(mapper.Map<IList<OrderViewDto>>(orders)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/order", async Task<Results<Created<OrderViewDto>, BadRequest>> (OrderDto orderDto, AppDbContext db, IMapper mapper) =>
        {
            Order? order = mapper.Map<Order>(orderDto);
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/order/{order.Guid}", mapper.Map<OrderViewDto>(order));
        })
        .WithOpenApi();

        group.MapPut("/order/{Guid}", async Task<Results<Ok<OrderViewDto>, NotFound, BadRequest>> (Guid guid, OrderDto orderDto, AppDbContext db, IMapper mapper) =>
        {
            Order? order = await db.Orders.FirstOrDefaultAsync(x => x.Guid == guid);
            if (order == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(orderDto, order);
            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<OrderViewDto>(order));
        })
        .WithOpenApi();

        group.MapDelete("/order/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            Order? order = await db.Orders.FirstOrDefaultAsync(x => x.Guid == guid);
            if (order == null)
            {
                return TypedResults.NotFound();
            }

            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();
    }

    private static void MapCategoryEndpoints(RouteGroupBuilder group)
    {
        group.MapGet("/category/{Guid}", async Task<Results<Ok<CategoryViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            Category? category = await db.Categories.FirstOrDefaultAsync(x => x.Guid == guid);
            return category != null ? TypedResults.Ok(mapper.Map<CategoryViewDto>(category)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/category/all", async Task<Results<Ok<IList<CategoryViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<Category> categorys = await db.Categories.ToListAsync();
            return categorys.Any() ? TypedResults.Ok(mapper.Map<IList<CategoryViewDto>>(categorys)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/category/paged", async Task<Results<Ok<IList<CategoryViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<Category> categorys = await db.Categories
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return categorys.Any() ? TypedResults.Ok(mapper.Map<IList<CategoryViewDto>>(categorys)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/category", async Task<Results<Created<CategoryViewDto>, BadRequest>> (CategoryDto categoryDto, AppDbContext db, IMapper mapper) =>
        {
            Category? category = mapper.Map<Category>(categoryDto);
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/category/{category.Guid}", mapper.Map<CategoryViewDto>(category));
        })
        .WithOpenApi();

        group.MapPut("/category/{Guid}", async Task<Results<Ok<CategoryViewDto>, NotFound, BadRequest>> (Guid guid, CategoryDto categoryDto, AppDbContext db, IMapper mapper) =>
        {
            Category? category = await db.Categories.FirstOrDefaultAsync(x => x.Guid == guid);
            if (category == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(categoryDto, category);
            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<CategoryViewDto>(category));
        })
        .WithOpenApi();

        group.MapDelete("/category/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            Category? category = await db.Categories.FirstOrDefaultAsync(x => x.Guid == guid);
            if (category == null)
            {
                return TypedResults.NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();
    }

}
