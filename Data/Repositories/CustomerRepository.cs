using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository

{
    public async Task<CustomerEntity?> GetCustomerWithDetailsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            return await _context.Customers
                .Include(c => c.Projects)
                .Include(c => c.CustomerType)
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(expression);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid hämtning av kund med detaljer: {ex.Message}");
            return null;
        }
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersWithProjectsAsync()
    {
        try
        {
            return await _context.Customers
                .Include(c => c.Projects)
                .Include(c => c.CustomerType)
                .Include(c => c.Contacts)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid hämtning av kunder med projekt: {ex.Message}");
            return Enumerable.Empty<CustomerEntity>();
        }
    }

}