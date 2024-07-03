using BlazorApp.ApiServices;
using BlazorApp.Dtos;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Pages
{
	public partial class Home : ComponentBase
	{
		[Inject] private IApiService<ProductViewDto> _productsApiService { get; set; }

		private ICollection<ProductViewDto>? _products = null;
		private int _page = 0, _totalPages = 0;

		protected override async Task OnInitializedAsync()
		{
			await LoadProducts();

			_totalPages = await _productsApiService.GetTotalPages();
		}

		private async Task LoadProducts()
		{
            _products = await _productsApiService.Get(_page);
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
