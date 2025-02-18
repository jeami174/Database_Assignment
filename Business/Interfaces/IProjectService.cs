using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        abstract Task<ProjectModel> CreateProjectAsync(ProjectCreateDto dto);
        abstract Task<ProjectModel> UpdateProjectAsync(int id, ProjectUpdateDto dto);
        abstract Task DeleteProjectAsync(int id);
        abstract Task<ProjectModel?> GetProjectWithDetailsAsync(int id);
        abstract Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
}