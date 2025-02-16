using Business.Dtos;
using Business.Factories;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;
public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserModel> CreateUserAsync(UserCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.FirstName) ||
            string.IsNullOrWhiteSpace(dto.LastName) ||
            string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new ArgumentException("Alla fält måste vara ifyllda och inte enbart whitespace.");
        }

        dto.Email = dto.Email.ToLower();

        bool exists = await _userRepository.ExistsAsync(u => u.Email.ToLower() == dto.Email);
        if (exists)
            throw new Exception("En användare med samma email finns redan.");

        try
        {
            var userEntity = UserFactory.CreateUserEntity(dto);
            var createdEntity = await _userRepository.CreateAsync(userEntity);
            return UserFactory.CreateUserModel(createdEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid skapande av user: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        var entities = await _userRepository.GetUsersWithDetailsAsync();
        return entities.Select(e => UserFactory.CreateUserModel(e));
    }
    public async Task<UserModel?> GetUserWithDetailsAsync(int id)
    {
        var entity = await _userRepository.GetUserWithDetailsAsync(u => u.Id == id);
        return entity != null ? UserFactory.CreateUserModel(entity) : null;
    }

    public async Task UpdateUserAsync(int id, UserUpdateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        dto.Email = dto.Email.ToLower();

        var entity = await _userRepository.GetOneAsync(u => u.Id == id);
        if (entity == null)
            throw new Exception("User not found");

        var updatedEntity = UserFactory.UpdateUserEntity(entity, dto);
        await _userRepository.UpdateAsync(updatedEntity);
    }

    public async Task DeleteUserAsync(int id)
    {
        var entity = await _userRepository.GetOneAsync(u => u.Id == id);
        if (entity == null)
            throw new Exception("User not found");
        await _userRepository.DeleteAsync(entity);
    }
}
