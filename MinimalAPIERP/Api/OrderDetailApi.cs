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

internal static class OrderDetailApi
{
    public static RouteGroupBuilder MapOrderDetailApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/erp")
            .WithTags("OrderDetail Api");

        group.MapGet("/orderdetail/{Guid}", async Task<Results<Ok<OrderDetailViewDto>, NotFound>> (Guid guid, AppDbContext db, IMapper mapper) =>
        {
            OrderDetail? orderdetail = await db.OrderDetails
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.Guid == guid);
            return orderdetail != null ? TypedResults.Ok(mapper.Map<OrderDetailViewDto>(orderdetail)) : TypedResults.NotFound();
        })
       .WithOpenApi();

        group.MapGet("/orderdetail/all", async Task<Results<Ok<IList<OrderDetailViewDto>>, NotFound>> (AppDbContext db, IMapper mapper) =>
        {
            ICollection<OrderDetail> orderdetails = await db.OrderDetails
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .Include(x => x.Order)
                .ToListAsync();
            return orderdetails.Any() ? TypedResults.Ok(mapper.Map<IList<OrderDetailViewDto>>(orderdetails)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapGet("/orderdetail/totalPages", async Task<Ok<int>> (AppDbContext db, IMapper mapper, int pageSize = 10) =>
        {
            int totalPages = (int)Math.Ceiling(await db.OrderDetails.CountAsync() / (float)pageSize) - 1;
            return TypedResults.Ok(totalPages);
        })
        .WithOpenApi();

        group.MapGet("/orderdetail/paged", async Task<Results<Ok<IList<OrderDetailViewDto>>, NotFound>> (AppDbContext db, IMapper mapper, int pageSize = 10, int page = 0) =>
        {
            ICollection<OrderDetail> orderdetails = await db.OrderDetails
                .Include(x => x.Product)
                .ThenInclude(X => X.Category)
                .Include(x => x.Order)
                .OrderBy(x => x.Guid)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return orderdetails.Any() ? TypedResults.Ok(mapper.Map<IList<OrderDetailViewDto>>(orderdetails)) : TypedResults.NotFound();
        })
        .WithOpenApi();

        group.MapPost("/orderdetail", async Task<Results<Created<OrderDetailViewDto>, BadRequest, NotFound>> (OrderDetailDto orderdetailDto, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == orderdetailDto.ProductGuid);
            if (product is null)
            {
                return TypedResults.NotFound();
            }

            Order? order = await db.Orders.FirstOrDefaultAsync(x => x.Guid == orderdetailDto.OrderGuid);
            if (order is null)
            {
                return TypedResults.NotFound();
            }

            OrderDetail? orderdetail = mapper.Map<OrderDetail>(orderdetailDto);
            orderdetail.Product = product;
            orderdetail.Order = order;

            db.OrderDetails.Add(orderdetail);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/erp/orderdetail/{orderdetail.Guid}", mapper.Map<OrderDetailViewDto>(orderdetail));
        })
        .WithOpenApi();

        group.MapPut("/orderdetail/{Guid}", async Task<Results<Ok<OrderDetailViewDto>, NotFound, BadRequest>> (Guid guid, OrderDetailDto orderdetailDto, AppDbContext db, IMapper mapper) =>
        {
            Product? product = await db.Products.FirstOrDefaultAsync(x => x.Guid == orderdetailDto.ProductGuid);
            if (product is null)
            {
                return TypedResults.NotFound();
            }

            Order? order = await db.Orders.FirstOrDefaultAsync(x => x.Guid == orderdetailDto.OrderGuid);
            if (order is null)
            {
                return TypedResults.NotFound();
            }

            OrderDetail? orderdetail = await db.OrderDetails.FirstOrDefaultAsync(x => x.Guid == guid);
            if (orderdetail == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(orderdetailDto, orderdetail);
            orderdetail.Product = product;
            orderdetail.Order = order;

            await db.SaveChangesAsync();
            return TypedResults.Ok(mapper.Map<OrderDetailViewDto>(orderdetail));
        })
        .WithOpenApi();

        group.MapDelete("/orderdetail/{Guid}", async Task<Results<NoContent, NotFound>> (Guid guid, AppDbContext db) =>
        {
            OrderDetail? orderdetail = await db.OrderDetails.FirstOrDefaultAsync(x => x.Guid == guid);
            if (orderdetail == null)
            {
                return TypedResults.NotFound();
            }

            db.OrderDetails.Remove(orderdetail);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        })
        .WithOpenApi();

        return group;
    }
}
