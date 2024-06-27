namespace MinimalAPIERP.Data.Repository
{
    public interface IRepositoryBase<TEntity>
    {
        public void Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
        public TEntity? Get(Guid guid);
        public ICollection<TEntity> Get(int page, int pageSize);
    }
}
