namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task BeginAsync<T>() where T : class;
        Task CommitAsync();
        Task RollbackAsync();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
