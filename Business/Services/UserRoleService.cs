using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class UserRoleService : BaseService<UserRoleModel, UserRoleEntity, UserRoleDto>, IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository repository)
            : base(repository, UserRoleFactory.ModelFromEntity, UserRoleFactory.EntityFromModel, UserRoleFactory.EntityFromDto)
        {
            _userRoleRepository = repository;
        }

        public async Task<ICollection<UserRoleModel>> GetAllUserRolesAsync()
        {
            var entities = await _userRoleRepository.GetAsync();
            return entities.Select(UserRoleFactory.ModelFromEntity).ToList();
        }
    }
}
