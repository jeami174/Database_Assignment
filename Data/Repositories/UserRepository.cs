using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }

    public async Task<UserEntity?> GetUserWithDetailsAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Projects)
                .FirstOrDefaultAsync(expression);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel i GetUserWithDetailsAsync: {ex.Message}");
            return null;
        }
    }

    public async Task<ICollection<UserEntity>> GetUsersWithDetailsAsync()
    {
        try
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Projects)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel i GetUsersWithDetailsAsync: {ex.Message}");
            return new List<UserEntity>();
        }
    }

    public async Task<ICollection<UserEntity>> GetUsersWithDetailsAsync(Expression<Func<UserEntity, bool>> expression)
    {
        try
        {
            return await _context.Users
                .Where(expression)
                .Include(u => u.Role)
                .Include(u => u.Projects)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel i GetUsersWithDetailsAsync (med uttryck): {ex.Message}");
            return new List<UserEntity>();
        }
    }
}
