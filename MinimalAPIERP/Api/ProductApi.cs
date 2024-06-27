using ERP.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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


        // TODO: Mover a config
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            //PropertyNameCaseInsensitive = false,
            //PropertyNamingPolicy = null,
            WriteIndented = true,
            //IncludeFields = false,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            //ReferenceHandler = ReferenceHandler.Preserve
        };

        group.MapGet("/products", async (AppDbContext db) =>
            await db.Products.ToListAsync()
                is IList<Product> products
                    ? Results.Json(products, options)
                    : Results.NotFound())
            .WithOpenApi();

        return group;
    }
}
