using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
    public async Task<IEnumerable<ServiceEntity>> GetServicesWithUnitsAsync()
    {
        return await _dbSet
            .Include(s => s.Unit)
            .ToListAsync();
    }

    public async Task<ServiceEntity?> GetServiceWithUnitAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        return await _dbSet
            .Include(s => s.Unit)
            .FirstOrDefaultAsync(expression);
    }

    public async Task<bool> AddServiceToProjectAsync(int projectId, ServiceEntity service)
    {
        try
        {
            service.ProjectId = projectId;
            _dbSet.Add(service);

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding service to project: {ex.Message}");
            return false;
        }
    }
}
