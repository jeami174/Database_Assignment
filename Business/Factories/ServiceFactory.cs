using System;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
{
    public static ServiceModel ModelFromEntity(ServiceEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new ServiceModel
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            PricePerHour = entity.PricePerHour,
            UnitName = entity.Unit?.UnitName ?? string.Empty
        };
    }

    public static ServiceEntity EntityFromModel(ServiceModel model)
    {
        return new ServiceEntity
        {
            Id = model.Id,
            ServiceName = model.ServiceName,
            PricePerHour = model.PricePerHour
        };
    }

    public static ServiceEntity EntityFromDto(ServiceDto dto)
    {
        return new ServiceEntity
        {
            ServiceName = dto.ServiceName,
            PricePerHour = dto.PricePerHour,
            UnitId = dto.UnitId
        };
    }
}
