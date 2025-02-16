using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class ProjectService : BaseService<ProjectModel, ProjectEntity, ProjectCreateDto>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository repository)
            : base(repository, ProjectFactory.ModelFromEntity, ProjectFactory.EntityFromModel, ProjectFactory.EntityFromDto)
        {
            _projectRepository = repository;
        }

        public async Task<ProjectModel> GetProjectDetailsAsync(int id)
        {
            var entity = await _projectRepository.GetProjectWithDetailsAsync(id);
            return entity != null ? ProjectFactory.ModelFromEntity(entity) : null!;
        }

        public async Task<ICollection<ProjectModel>> GetAllProjectsWithDetailsAsync()
        {
            var entities = await _projectRepository.GetAllProjectsWithDetailsAsync();
            return entities.Select(ProjectFactory.ModelFromEntity).ToList();
        }
    }
}
