using Business.Models;
using Data.Entities;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IUserService : IBaseService<UserModel, UserEntity, UserCreateDto>
{
    Task<UserModel> GetUserWithDetailsAsync(int id);
}
