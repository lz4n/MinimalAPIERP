using ERP;
using ERP.Data;
using Microsoft.EntityFrameworkCore;
using MinimalAPIERP.Dtos;

namespace MinimalAPIERP.Data.Repository
{
    public class ProductRepository : IRepositoryBase<Product>
    {
        private AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(Product entity)
        {
            _appDbContext.Products.Add(entity);
        }

        public void Delete(Product entity)
        {
            _appDbContext.Products.Remove(entity);
        }

        public void Update(Product entity)
        {
            _appDbContext.Products.Update(entity);
        }

        public ICollection<Product> Get(int page, int pageSize)
        {
            return _appDbContext.Products
                .OrderBy(p => p.ProductId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Product? Get(Guid guid)
        {
            return _appDbContext.Products.Find(guid);
        }
    }
}
