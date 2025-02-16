using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class UserRoleService(IUserRoleRepository userRoleRepository) : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;

    public async Task<UserRoleModel> CreateUserRoleAsync(UserRoleCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        try
        {
            var entity = UserRoleFactory.CreateUserRoleEntity(dto);
            var createdEntity = await _userRoleRepository.CreateAsync(entity);
            return UserRoleFactory.CreateUserRoleModel(createdEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating user role: {ex.Message}");
            throw;
        }
    }
    public async Task<IEnumerable<UserRoleModel>> GetAllUserRolesAsync()
    {
        try
        {
            var entities = await _userRoleRepository.GetAllAsync();
            return entities.Select(e => UserRoleFactory.CreateUserRoleModel(e));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving user roles: {ex.Message}");
            throw;
        }
    }

    public async Task<UserRoleModel?> GetUserRoleByIdAsync(int id)
    {
        try
        {
            var entity = await _userRoleRepository.GetOneAsync(x => x.Id == id);
            return entity != null ? UserRoleFactory.CreateUserRoleModel(entity) : null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving user role by id: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateUserRoleAsync(int id, UserRoleUpdateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        try
        {
            var entity = await _userRoleRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                throw new Exception("User role not found");

            var updatedEntity = UserRoleFactory.UpdateUserRoleEntity(entity, dto);
            await _userRoleRepository.UpdateAsync(updatedEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating user role: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteUserRoleAsync(int id)
    {
        try
        {
            var entity = await _userRoleRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                throw new Exception("User role not found");

            await _userRoleRepository.DeleteAsync(entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting user role: {ex.Message}");
            throw;
        }
    }
}
