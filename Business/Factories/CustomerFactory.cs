using System;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerModel ModelFromEntity(CustomerEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new CustomerModel
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            CustomerType = entity.CustomerType?.CustomerTypeName ?? string.Empty
        };
    }

    public static CustomerEntity EntityFromModel(CustomerModel model)
    {
        return new CustomerEntity
        {
            Id = model.Id,
            CustomerName = model.CustomerName
        };
    }

    public static CustomerEntity EntityFromDto(CustomerDto dto)
    {
        return new CustomerEntity
        {
            CustomerName = dto.CustomerName,
            CustomerTypeId = dto.CustomerTypeId
        };
    }
}
