using System.Threading.Tasks;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class UserService : BaseService<UserModel, UserEntity, UserRegistrationForm>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
            : base(repository, UserFactory.ModelFromEntity, UserFactory.EntityFromModel, UserFactory.EntityFromDto)
        {
            _userRepository = repository;
        }

        public async Task<UserModel> GetUserWithDetailsAsync(int id)
        {
            var entity = await _userRepository.GetUserWithDetailsAsync(u => u.Id == id);
            return entity != null ? UserFactory.ModelFromEntity(entity) : null!;
        }
    }
}
