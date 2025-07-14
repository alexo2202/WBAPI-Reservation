using Domain.Repositories;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private PersistenceContext _context;
        private IDbContextTransaction? _transaction;
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly IDbContextFactory<PersistenceContext> _contextFactory;

        public UnitOfWork(IDbContextFactory<PersistenceContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _context = _contextFactory.CreateDbContext();
            _repositories = new Dictionary<Type, object>();
        }

        public async Task BeginAsync<T>() where T : class
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if(_transaction is null)
            {
                throw new InvalidOperationException("There is not a transaction.");
            }
            await _transaction.CommitAsync();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<TEntity>(_context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task RollbackAsync()
        {
            if (_transaction is null)
            {
                throw new InvalidOperationException("There is not a transaction.");
            }
            await _transaction.RollbackAsync();
        }
    }
}
