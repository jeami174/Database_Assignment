using Business.Models;
using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IProjectService : IBaseService<ProjectModel, ProjectEntity, ProjectDto>
{
    Task<ProjectModel> GetProjectDetailsAsync(int id);
    Task<ICollection<ProjectModel>> GetAllProjectsWithDetailsAsync();
}
