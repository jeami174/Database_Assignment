using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating {nameof(TEntity)} entity: {ex.Message}");
            throw;
        }
    }


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async virtual Task<IEnumerable<TEntity>> GetAllWithDetailsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression)
    {
        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }
        return await query.ToListAsync();
    }

    public virtual async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _dbSet.FirstOrDefaultAsync(expression);

            if (entity == null)
            {
                Debug.WriteLine($"No {nameof(TEntity)} entity found matching the criteria.");
            }

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving {nameof(TEntity)} entity: {ex.Message}");
            return null;
        }
    }

    public virtual async Task<TEntity?> GetOneWithDetailsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeExpression, Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        IQueryable<TEntity> query = _dbSet;
        if (includeExpression != null)
        {
            query = includeExpression(query);
        }

        var entity = await query.FirstOrDefaultAsync(predicate);
        return entity;

    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting {nameof(TEntity)} entity :: {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        try
        {
            var result = await _dbSet.AnyAsync(expression);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error checking existence of {nameof(TEntity)}: {ex.Message}");
            return false;
        }
    }




}