using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserRoleService : IBaseService<UserRoleModel, UserRoleEntity, UserRoleDto>
    {
        Task<ICollection<UserRoleModel>> GetAllUserRolesAsync();
    }
}

