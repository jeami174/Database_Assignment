using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectModel> CreateProjectAsync(ProjectCreateDto dto);
        Task<ProjectModel> UpdateProjectAsync(int id, ProjectUpdateDto dto);
        Task DeleteProjectAsync(int id);
        Task<ProjectModel?> GetProjectWithDetailsAsync(int id);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
}