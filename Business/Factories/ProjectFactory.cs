using System;
using System.Linq;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectModel ModelFromEntity(ProjectEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new ProjectModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description?.Length > 50 ? entity.Description.Substring(0, 50) + "..." : entity.Description,
            FullDescription = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Status = entity.Status?.StatusTypeName ?? string.Empty,
            ServiceName = entity.Service?.ServiceName ?? string.Empty,
            CustomerName = entity.Customer?.CustomerName ?? string.Empty,
            UserName = $"{entity.User?.FirstName} {entity.User?.LastName}",
            UserRole = entity.User?.Role?.RoleName ?? string.Empty,
            ServicePrice = entity.Service?.PricePerHour ?? 0,
            UnitName = entity.Service?.Unit?.UnitName ?? string.Empty,
            CustomerContact = entity.Customer?.Contacts.FirstOrDefault() is var contact && contact != null
                                ? $"{contact.FirstName} {contact.LastName}"
                                : string.Empty
        };
    }

    public static ProjectEntity EntityFromModel(ProjectModel model)
    {
        return new ProjectEntity
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };
    }

    public static ProjectEntity EntityFromDto(ProjectDto dto)
    {
        return new ProjectEntity
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CustomerId = dto.CustomerId,
            ServiceId = dto.ServiceId,
            StatusId = dto.StatusId,
            UserId = dto.UserId
        };
    }
}
