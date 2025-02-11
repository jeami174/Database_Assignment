using Business.Models;
using Data.Entities;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IUserService : IBaseService<UserModel, UserEntity, UserDto>
{
    Task<UserModel> GetUserWithDetailsAsync(int id);
}
