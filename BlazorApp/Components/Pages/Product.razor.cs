using BlazorApp.ApiServices;
using BlazorApp.Dtos;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Pages
{
    public partial class Product
    {
        [Inject] private ICartItemApiService cartItemApiService { get; set; }
        [Inject] private IProductsApiService productsApiService { get; set; }

        [Parameter]
        public string? ProductGuid { get; set; }

        private ProductDto product = null;
        private string loadingText = "Cargando...";

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            if (string.IsNullOrWhiteSpace(ProductGuid) || !Guid.TryParse(ProductGuid, out var productGuid)) {
                loadingText = "Identificación de producto inválida.";

                return;
            }

            try
            {               
                product = await productsApiService.GetProduct(productGuid);
            }
            catch (HttpRequestException)
            {
                loadingText = "No se han podido leer el producto.";
            }
        }

        private async Task AddToCart()
        {
            await cartItemApiService.AddProductToCart(product);
        }
    }
}
