using AutoMapper;
using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Dtos;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERP.Api;

internal static class StoreApi
{
    public static RouteGroupBuilder MapStoreApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Store Api");

        group.MapGet("/user", (ClaimsPrincipal user) =>
        {
            return user.Identity;
        })
        .WithOpenApi();

        group.MapGet("/store/{Guid}", async Task<Results<Ok<StoreViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            Store? store = await db.Stores.FirstOrDefaultAsync(x => x.Guid == guid);
            return store != null ? TypedResults.Ok(mapper.Map<StoreViewDto>(store)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/store/all", async Task<Results<Ok<IList<StoreViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<Store> stores = await db.Stores.ToListAsync();
            return stores.Any() ? TypedResults.Ok(mapper.Map<IList<StoreViewDto>>(stores)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/store/totalPages", async Task<Ok<int>> (AppDbContext db, IMapper mapper, int pageSize = 10) =>
        {
            int totalPages = (int)Math.Ceiling(await db.Stores.CountAsync() / (float)pageSize) - 1;
            return TypedResults.Ok(totalPages);
        })
        .WithOpenApi();

        group.MapGet("/store/paged", async Task<Results<Ok<IList<StoreViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<Store> stores = await db.Stores
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return stores.Any() ? TypedResults.Ok(mapper.Map<IList<StoreViewDto>>(stores)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/store", async Task<Results<Created<StoreViewDto>, BadRequest>> (StoreDto storeDto, AppDbContext db, IMapper mapper) =>
        {
            Store? store = mapper.Map<Store>(storeDto);
            db.Stores.Add(store);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/store/{store.Guid}", mapper.Map<StoreViewDto>(store));
        })
        .WithOpenApi();

        group.MapPut("/store/{Guid}", async Task<Results<Ok<StoreViewDto>, NotFound, BadRequest>> (Guid guid, StoreDto storeDto, AppDbContext db, IMapper mapper) =>
        {
            Store? store = await db.Stores.FirstOrDefaultAsync(x => x.Guid == guid);
            if (store == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(storeDto, store);
            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<StoreViewDto>(store));
        })
        .WithOpenApi();

        group.MapDelete("/store/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            Store? store = await db.Stores.FirstOrDefaultAsync(x => x.Guid == guid);
            if (store == null)
            {
                return TypedResults.NotFound();
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();

        return group;
    }
}
