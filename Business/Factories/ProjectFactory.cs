using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class ProjectFactory
    {
        /// <summary>
        /// Skapar en ProjectModel från en ProjectEntity.
        /// Används när man har fullständig data (t.ex. vid GetProjectWithDetailsAsync).
        /// </summary>
        public static ProjectModel CreateProjectModel(ProjectEntity entity)
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

        /// <summary>
        /// Skapar en ProjectModel från en ProjectDto.
        /// Eftersom DTO:t endast innehåller en del av fälten (för UI) så fyller vi i default-värden för de övriga.
        /// </summary>
        public static ProjectModel CreateProjectModel(ProjectDto dto)
        {
            return new ProjectModel
            {
                Id = dto.Id,
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                // Eftersom DTO:t inte innehåller dessa fält sätts defaultvärden
                Description = string.Empty,
                TotalPrice = 0,
                // Dessa kan antingen vara tomma modeller eller hämtas separat via andra tjänster
                Customer = new CustomerModel(),
                Service = new ServiceModel(),
                Status = new StatusTypeModel(),
                User = new UserModel()
            };
        }

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

        public static ProjectEntity UpdateProjectEntity(ProjectEntity projectEntity, ProjectUpdateDto dto)
        {
            projectEntity.Title = dto.Title;
            projectEntity.Description = dto.Description;
            projectEntity.StartDate = dto.StartDate;
            projectEntity.EndDate = dto.EndDate;
            projectEntity.TotalPrice = dto.TotalPrice;
            projectEntity.StatusId = dto.StatusId;
            projectEntity.UserId = dto.UserId;

            return projectEntity;
        }
    }
}
