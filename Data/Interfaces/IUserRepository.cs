using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        /// Hämtar en användare med roll och kopplade projekt baserat på ett givet villkor.
        Task<UserEntity?> GetUserWithDetailsAsync(Expression<Func<UserEntity, bool>> expression);

        /// Hämtar en lista med alla användare där varje användare har roll och kopplade projekt inkluderade.
        Task<ICollection<UserEntity>> GetUsersWithDetailsAsync();

        /// Hämtar en lista med användare baserat på ett specifikt villkor, inklusive roll och kopplade projekt.
        Task<ICollection<UserEntity>> GetUsersWithDetailsAsync(Expression<Func<UserEntity, bool>> expression);
    }
}