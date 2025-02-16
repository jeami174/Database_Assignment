using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleModel> CreateUserRoleAsync(UserRoleCreateDto dto);
        Task<IEnumerable<UserRoleModel>> GetAllUserRolesAsync();
        Task<UserRoleModel?> GetUserRoleByIdAsync(int id);
        Task UpdateUserRoleAsync(int id, UserRoleUpdateDto dto);
        Task DeleteUserRoleAsync(int id);
    }
}