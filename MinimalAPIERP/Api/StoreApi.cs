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

        group.MapGet("/store/{Guid}", async Task<Results<Ok<StoreViewDto>, NotFound>> (Guid Guid, AppDbContext db, IMapper mapper) =>
        {
            var store = await db.Stores.FirstOrDefaultAsync(m => m.Guid == Guid);
            return store != null ? TypedResults.Ok(mapper.Map<StoreViewDto>(store)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/store/all", async Task<Results<Ok<IList<StoreViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            var stores = await db.Stores.ToListAsync();
            return stores.Any() ? TypedResults.Ok(mapper.Map<IList<StoreViewDto>>(stores)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/store/paged", async Task<Results<Ok<IList<StoreViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            var stores = await db.Stores
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return stores.Any() ? TypedResults.Ok(mapper.Map<IList<StoreViewDto>>(stores)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/store", async Task<Results<Created<StoreViewDto>, BadRequest>> (StoreDto storeDto, AppDbContext db, IMapper mapper) =>
        {
            var store = mapper.Map<Store>(storeDto);
            db.Stores.Add(store);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/store/{store.Guid}", mapper.Map<StoreViewDto>(store));
        })
        .WithOpenApi();

        group.MapPut("/store/{Guid}", async Task<Results<Ok<StoreViewDto>, NotFound, BadRequest>> (Guid Guid, StoreDto storeDto, AppDbContext db, IMapper mapper) =>
        {
            var store = await db.Stores.FirstOrDefaultAsync(m => m.Guid == Guid);
            if (store == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(storeDto, store);
            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<StoreViewDto>(store));
        })
        .WithOpenApi();

        group.MapDelete("/store/{Guid}", async Task<Results<NoContent, NotFound>> (Guid Guid, AppDbContext db) =>
        {
            var store = await db.Stores.FirstOrDefaultAsync(m => m.Guid == Guid);
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
