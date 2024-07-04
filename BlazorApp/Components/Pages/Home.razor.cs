using BlazorApp.ApiServices;
using BlazorApp.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Data;

namespace BlazorApp.Components.Pages
{
	public partial class Home : ComponentBase
	{
		[Inject] private ICartItemApiService cartItemApiService { get; set; }
		[Inject] private IProductsApiService productsApiService { get; set; }

		private ICollection<ProductDto>? products = null;
		private string loadingText = "Cargando...";
		private int page = 0, totalPages = 0;

		protected override async Task OnInitializedAsync()
		{
			await LoadProducts();

			totalPages = await productsApiService.GetProductsTotalPages();
		}

		private async Task LoadProducts()
		{
			try
			{
                products = await productsApiService.GetProducts(page);
            } catch (HttpRequestException)
			{
				loadingText = "No se han podido leer los productos.";
			}    
        }

        private async Task AddProductToCart(ProductDto product)
        {
            await cartItemApiService.AddProductToCart(product);
        }

        private async Task NextPage()
		{
			page++;

            await LoadProducts();
        }
		private async Task PreviousPage()
		{
			page--;

            await LoadProducts();
        }
    }
}
