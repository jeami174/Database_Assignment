using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository

{
    public async Task<CustomerEntity?> GetCustomerWithDetailsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _context.Customers
            .Include(c => c.Projects)
            .FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersWithProjectsAsync()
    {
        return await _context.Customers
            .Include(c => c.Projects)
            .ToListAsync();
    }
}