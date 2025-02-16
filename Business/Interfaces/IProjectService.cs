using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IProjectService : IBaseService<ProjectModel, ProjectEntity, ProjectCreateDto>
{
    Task<ProjectModel> GetProjectDetailsAsync(int id);
    Task<ICollection<ProjectModel>> GetAllProjectsWithDetailsAsync();
}
