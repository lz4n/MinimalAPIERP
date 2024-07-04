using BlazorApp.ApiServices;
using BlazorApp.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Data;

namespace BlazorApp.Components.Pages
{
	public partial class Home : ComponentBase
	{
		[Inject] private ICartItemApiService _cartItemApiService { get; set; }
		[Inject] private IProductsApiService _productsApiService { get; set; }

		private ICollection<ProductDto>? _products = null;
		private string loadingText = "Cargando...";
		private int _page = 0, _totalPages = 0;

		protected override async Task OnInitializedAsync()
		{
			await LoadProducts();

			_totalPages = await _productsApiService.GetProductsTotalPages();
		}

		private async Task LoadProducts()
		{
			try
			{
                _products = await _productsApiService.GetProducts(_page);
            } catch (HttpRequestException)
			{
				loadingText = "No se han podido leer los productos.";
			}    
        }

        private async Task AddProductToCart(ProductDto product)
        {
            await _cartItemApiService.AddProductToCart(product);
        }

        private async Task NextPage()
		{
			_page++;

            await LoadProducts();
        }
		private async Task PreviousPage()
		{
			_page--;

            await LoadProducts();
        }
    }
}
