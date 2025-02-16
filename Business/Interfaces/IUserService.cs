using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> CreateUserAsync(UserCreateDto dto);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetUserWithDetailsAsync(int id);
        Task UpdateUserAsync(int id, UserUpdateDto dto);
        Task DeleteUserAsync(int id);
    }
}
