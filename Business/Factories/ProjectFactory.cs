using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    // <summary>
    /// Skapar en ProjectEntity från ett ProjectCreateDto.
    /// </summary>
    public static ProjectEntity CreateProjectEntity(ProjectCreateDto dto)
    {
        return new ProjectEntity
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            TotalPrice = dto.TotalPrice,
            CustomerId = dto.CustomerId,
            ServiceId = dto.ServiceId,
            StatusId = dto.StatusId,
            UserId = dto.UserId
        };
    }

    /// <summary>
    /// Uppdaterar en befintlig ProjectEntity med data från ett ProjectUpdateDto.
    /// </summary>
    public static ProjectEntity UpdateProjectEntity(ProjectEntity entity, ProjectUpdateDto dto)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
        entity.TotalPrice = dto.TotalPrice;
        entity.StatusId = dto.StatusId;
        entity.UserId = dto.UserId;
        return entity;
    }

    /// <summary>
    /// Skapar en översiktsmodell (ProjectModel) från en ProjectEntity med minimala fält för UI:t.
    /// Endast de fält som behövs för översikten mappas.
    /// </summary>
    public static ProjectModel CreateProjectOverviewModel(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = string.Empty,    // Ej med i översikten
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TotalPrice = 0,                // Ej med i översikten
            Customer = new CustomerModel
            {
                Id = entity.Customer.Id,
                CustomerName = entity.Customer.CustomerName,
                CustomerTypeId = entity.Customer.CustomerTypeId,
                CustomerContacts = new List<CustomerContactModel>() // Tom lista
            },
            // Vi hoppar över Service i översikten
            Service = new ServiceModel(),
            Status = new StatusTypeModel
            {
                Id = entity.Status.Id,
                StatusTypeName = entity.Status.StatusTypeName
            },
            User = new UserModel
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName,
                Email = entity.User.Email,
                RoleId = entity.User.RoleId,
                Role = new UserRoleModel
                {
                    Id = entity.User.Role.Id,
                    RoleName = entity.User.Role.RoleName
                }
            }
        };
    }

    /// <summary>
    /// Skapar en detaljerad modell (ProjectModel) från en ProjectEntity med all data.
    /// Används vid visning av ett enskilt projekt med fullständiga detaljer.
    /// </summary>
    public static ProjectModel CreateProjectDetailModel(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TotalPrice = entity.TotalPrice,
            Customer = CustomerFactory.CreateCustomerModel(entity.Customer),
            Service = ServiceFactory.CreateServiceModel(entity.Service),
            Status = StatusTypeFactory.CreateStatusTypeModel(entity.Status),
            User = UserFactory.CreateUserModel(entity.User)
        };
    }
}
  
