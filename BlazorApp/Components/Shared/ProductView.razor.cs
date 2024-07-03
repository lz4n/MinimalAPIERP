using BlazorApp.ApiServices;
using Microsoft.AspNetCore.Components;
using BlazorApp.Dtos;

namespace BlazorApp.Components.Shared
{
    public partial class ProductView
    {
        [Parameter]
        public ProductViewDto Product { get; set; }
    }
}
