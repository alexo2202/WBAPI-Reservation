using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        //Get methods
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string? includeString = null,
        bool disableTracking = true,
        int pageNumber = 1,
        int pageSize = 10);

        Task<IReadOnlyList<T>> GetMulipleAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true
        );

        Task<T> GetByIdAsync(int id);

        //Add Methods
        Task<T> AddAsync(T entity);

        //Update Methods
        Task<T> UpdateAsync(T entity);

        //Delete Methods
        Task DeleteAsync(T entity);
    }
}
