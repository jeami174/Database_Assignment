﻿using System;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static StatusTypeModel ModelFromEntity(StatusTypeEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new StatusTypeModel
        {
            Id = entity.Id,
            StatusTypeName = entity.StatusTypeName
        };
    }

    public static StatusTypeEntity EntityFromModel(StatusTypeModel model)
    {
        return new StatusTypeEntity
        {
            Id = model.Id,
            StatusTypeName = model.StatusTypeName
        };
    }

    public static StatusTypeEntity EntityFromDto(StatusTypeDto dto)
    {
        return new StatusTypeEntity
        {
            StatusTypeName = dto.StatusTypeName
        };
    }
}
