using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllWithDetailsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetOneWithDetailsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression, Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
}