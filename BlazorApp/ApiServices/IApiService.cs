using BlazorApp.Dtos;

namespace BlazorApp.ApiServices
{
    public interface IApiService<IEntity>
    {
        private const int DefaultPageSize = 6;

        public Task<ICollection<IEntity>> Get(int page, int pageSize = DefaultPageSize);

        public Task<int> GetTotalPages(int pageSize = DefaultPageSize);
    }
}
