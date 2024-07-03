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

internal static class RaincheckApi
{
    public static RouteGroupBuilder MapRaincheckApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("Raincheck Api");

        group.MapGet("/raincheck/{Guid}", async Task<Results<Ok<RaincheckViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            Raincheck? raincheck = await db.Rainchecks
                .Include(x => x.Store)
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .FirstOrDefaultAsync(x => x.Guid == guid);
            return raincheck != null ? TypedResults.Ok(mapper.Map<RaincheckViewDto>(raincheck)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/raincheck/all", async Task<Results<Ok<IList<RaincheckViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<Raincheck> rainchecks = await db.Rainchecks
                .Include(x => x.Store)
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .ToListAsync();
            return rainchecks.Any() ? TypedResults.Ok(mapper.Map<IList<RaincheckViewDto>>(rainchecks)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/raincheck/totalPages", async Task<Ok<int>> (AppDbContext db, IMapper mapper, int pageSize = 10) =>
        {
            int totalPages = (int)Math.Ceiling(await db.Rainchecks.CountAsync() / (float)pageSize) - 1;
            return TypedResults.Ok(totalPages);
        })
        .WithOpenApi();

        group.MapGet("/raincheck/paged", async Task<Results<Ok<IList<RaincheckViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<Raincheck> rainchecks = await db.Rainchecks
                .Include(x => x.Store)
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return rainchecks.Any() ? TypedResults.Ok(mapper.Map<IList<RaincheckViewDto>>(rainchecks)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/raincheck", async Task<Results<Created<RaincheckViewDto>, BadRequest, NotFound>> (RaincheckDto raincheckDto, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == raincheckDto.ProductGuid);
            if (product is null)
            {
                return TypedResults.NotFound();
            }

            Store? store = await db.Stores.FirstOrDefaultAsync(x => x.Guid == raincheckDto.StoreGuid);
            if (store is null)
            {
                return TypedResults.NotFound();
            }

            Raincheck? raincheck = mapper.Map<Raincheck>(raincheckDto);
            raincheck.Product = product;
            raincheck.Store = store;

            db.Rainchecks.Add(raincheck);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/raincheck/{raincheck.Guid}", mapper.Map<RaincheckViewDto>(raincheck));
        })
        .WithOpenApi();

        group.MapPut("/raincheck/{Guid}", async Task<Results<Ok<RaincheckViewDto>, NotFound, BadRequest>> (Guid guid, RaincheckDto raincheckDto, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == raincheckDto.ProductGuid);
            if (product is null)
            {
                return TypedResults.NotFound();
            }

            Store? store = await db.Stores.FirstOrDefaultAsync(x => x.Guid == raincheckDto.StoreGuid);
            if (store is null)
            {
                return TypedResults.NotFound();
            }

            Raincheck? raincheck = await db.Rainchecks.FirstOrDefaultAsync(x => x.Guid == guid);
            if (raincheck == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(raincheckDto, raincheck);
            raincheck.Product = product;
            raincheck.Store = store;

            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<RaincheckViewDto>(raincheck));
        })
        .WithOpenApi();

        group.MapDelete("/raincheck/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            Raincheck? raincheck = await db.Rainchecks.FirstOrDefaultAsync(x => x.Guid == guid);
            if (raincheck == null)
            {
                return TypedResults.NotFound();
            }

            db.Rainchecks.Remove(raincheck);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();



        return group;
    }
}
