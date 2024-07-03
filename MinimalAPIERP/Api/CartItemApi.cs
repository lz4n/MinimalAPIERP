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

internal static class CartItemApi
{
    public static RouteGroupBuilder MapCartItemApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("CartItem Api");

        group.MapGet("/cartitem/{Guid}", async Task<Results<Ok<CartItemViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            CartItem? cartitem = await db.CartItems
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .FirstOrDefaultAsync(x => x.Guid == guid);
            return cartitem != null ? TypedResults.Ok(mapper.Map<CartItemViewDto>(cartitem)) : TypedResults.NotFound();
        })
       .WithOpenApi();

        group.MapGet("/cartitem/all", async Task<Results<Ok<IList<CartItemViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<CartItem> cartitems = await db.CartItems
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .ToListAsync();
            return cartitems.Any() ? TypedResults.Ok(mapper.Map<IList<CartItemViewDto>>(cartitems)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/cartitem/totalPages", async Task<Ok<int>> (AppDbContext db, IMapper mapper, int pageSize = 10) =>
        {
            int totalPages = (int)Math.Ceiling(await db.CartItems.CountAsync() / (float)pageSize) - 1;
            return TypedResults.Ok(totalPages);
        })
        .WithOpenApi();

        group.MapGet("/cartitem/paged", async Task<Results<Ok<IList<CartItemViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<CartItem> cartitems = await db.CartItems
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return cartitems.Any() ? TypedResults.Ok(mapper.Map<IList<CartItemViewDto>>(cartitems)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/cartitem", async Task<Results<Created<CartItemViewDto>, BadRequest, NotFound>> (CartItemDto cartitemDto, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == cartitemDto.ProductGuid);
            if (product is null)
            {
                return TypedResults.NotFound();
            }

            CartItem? cartitem = mapper.Map<CartItem>(cartitemDto);
            cartitem.Product = product;

            db.CartItems.Add(cartitem);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/cartitem/{cartitem.Guid}", mapper.Map<CartItemViewDto>(cartitem));
        })
        .WithOpenApi();

        group.MapPut("/cartitem/{Guid}", async Task<Results<Ok<CartItemViewDto>, NotFound, BadRequest>> (Guid guid, CartItemDto cartitemDto, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == cartitemDto.ProductGuid);
            if (product is null)
            {
                return TypedResults.NotFound();
            }

            CartItem? cartitem = await db.CartItems.FirstOrDefaultAsync(x => x.Guid == guid);
            if (cartitem == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(cartitemDto, cartitem);
            cartitem.Product = product;

            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<CartItemViewDto>(cartitem));
        })
        .WithOpenApi();

        group.MapDelete("/cartitem/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            CartItem? cartitem = await db.CartItems.FirstOrDefaultAsync(x => x.Guid == guid);
            if (cartitem == null)
            {
                return TypedResults.NotFound();
            }

            db.CartItems.Remove(cartitem);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();

        return group;
    }
}
