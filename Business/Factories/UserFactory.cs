using System;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserFactory
{
    public static UserModel ModelFromEntity(UserEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new UserModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            RoleName = entity.Role?.RoleName ?? string.Empty
        };
    }

    public static UserEntity EntityFromModel(UserModel model)
    {
        return new UserEntity
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };
    }

    public static UserEntity EntityFromDto(UserCreateDto dto)
    {
        return new UserEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            RoleId = dto.RoleId
        };
    }
}
