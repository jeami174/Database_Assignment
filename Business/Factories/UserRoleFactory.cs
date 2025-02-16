using System;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserRoleFactory
{
    public static UserRoleModel ModelFromEntity(UserRoleEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new UserRoleModel
        {
            Id = entity.Id,
            RoleName = entity.RoleName
        };
    }

    public static UserRoleEntity EntityFromModel(UserRoleModel model)
    {
        return new UserRoleEntity
        {
            Id = model.Id,
            RoleName = model.RoleName
        };
    }

    public static UserRoleEntity EntityFromDto(UserRoleDto dto)
    {
        return new UserRoleEntity
        {
            RoleName = dto.RoleName
        };
    }
}

