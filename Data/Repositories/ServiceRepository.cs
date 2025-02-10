using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
    {
        public async Task<ServiceEntity?> GetServiceWithUnitAsync(Expression<Func<ServiceEntity, bool>> expression)
        {
            try
            {
                return await _context.Services
                    .Include(s => s.Unit)
                    .FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fel vid hämtning av service med unit: {ex.Message}");
                return null;
            }
        }
    }
}
