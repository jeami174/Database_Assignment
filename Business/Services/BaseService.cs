using Business.Interfaces;
using Data.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Business.Services;

public class BaseService<TModel, TEntity, TDto>(
    IBaseRepository<TEntity> repository,
    Func<TEntity, TModel> modelFromEntity,
    Func<TModel, TEntity> entityFromModel,
    Func<TDto, TEntity> entityFromDto
) : IBaseService<TModel, TEntity, TDto>
    where TModel : class
    where TEntity : class, IEntity
    where TDto : class
{
    protected readonly IBaseRepository<TEntity> _repository = repository;
    protected readonly Func<TEntity, TModel> _modelFromEntity = modelFromEntity;
    protected readonly Func<TModel, TEntity> _entityFromModel = entityFromModel;
    protected readonly Func<TDto, TEntity> _entityFromDto = entityFromDto;

    public virtual async Task<TModel> CreateAsync(TDto dto)
    {
        if (dto == null)
            return null!;

        try
        {
            var entity = _entityFromDto(dto);
            var createdEntity = await _repository.AddAsync(entity);
            return createdEntity != null ? _modelFromEntity(createdEntity) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid skapande: {ex.Message}");
            throw;
        }
    }

    public virtual async Task<ICollection<TModel>> GetAllAsync()
    {
        var entities = await _repository.GetAsync();
        return entities.Select(e => _modelFromEntity(e)).ToList();
    }


    public virtual async Task<TModel> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _repository.GetAsync(expression);
        return entity != null ? _modelFromEntity(entity) : null!;
    }

    public virtual async Task<bool> UpdateAsync(int id, TModel model)
    {
        try
        {
            if (model == null)
                return false;

            var entity = _entityFromModel(model);
            var updatedEntity = await _repository.UpdateAsync(entity);
            return updatedEntity != null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid uppdatering: {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await _repository.GetAsync(e => ((IEntity)e).Id == id);
            if (entity == null)
                return false;
            return await _repository.DeleteAsync(entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid borttagning: {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return true;

        return await _repository.ExistsAsync(expression);
    }

    //Fått hjälp av ChatGTP nedan.

    public Expression<Func<TEntity, bool>> CreateExpression(string field, string value)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = typeof(TEntity).GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
            throw new ArgumentException($"Fältet {field} hittades inte i {typeof(TEntity).Name}");

        var convertedValue = Convert.ChangeType(value, property.PropertyType);
        var comparison = Expression.Equal(Expression.Property(parameter, property), Expression.Constant(convertedValue));
        return Expression.Lambda<Func<TEntity, bool>>(comparison, parameter);
    }
}
