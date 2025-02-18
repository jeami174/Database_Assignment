using Business.Dtos;
using Business.Factories;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Interfaces;

namespace Business.Services
{
    // Använder transaktionshantering för att hantera rollback vid fel.
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly DataContext _context;

        public ProjectService(IProjectRepository projectRepository, DataContext context)
        {
            _projectRepository = projectRepository;
            _context = context;
        }

        public async Task<ProjectModel> CreateProjectAsync(ProjectCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var projectEntity = ProjectFactory.CreateProjectEntity(dto);
                var createdEntity = await _projectRepository.CreateAsync(projectEntity);
                await transaction.CommitAsync();
                return ProjectFactory.CreateProjectModel(createdEntity);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Debug.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
        }

        public async Task<ProjectModel> UpdateProjectAsync(int id, ProjectUpdateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _projectRepository.GetOneAsync(p => p.Id == id);
                if (entity == null)
                    throw new Exception("Project not found");

                var updatedEntity = ProjectFactory.UpdateProjectEntity(entity, dto);
                var result = await _projectRepository.UpdateAsync(updatedEntity);
                if (result == null)
                    throw new Exception("Error updating project");

                await transaction.CommitAsync();
                return ProjectFactory.CreateProjectModel(result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Debug.WriteLine($"Error updating project: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProjectAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var entity = await _projectRepository.GetOneAsync(p => p.Id == id);
                if (entity == null)
                    throw new Exception("Project not found");

                var deleted = await _projectRepository.DeleteAsync(entity);
                if (!deleted)
                    throw new Exception("Error deleting project");

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Debug.WriteLine($"Error deleting project: {ex.Message}");
                throw;
            }
        }

        public async Task<ProjectModel?> GetProjectWithDetailsAsync(int id)
        {
            // Läsoperationer kräver inte en explicit transaktion
            var entity = await _projectRepository.GetProjectWithDetailsAsync(id);
            return entity != null ? ProjectFactory.CreateProjectModel(entity) : null;
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            try
            {
                // Hämta entiteter med detaljer
                var projectEntities = await _projectRepository.GetAllProjectsWithDetailsAsync();
                if (projectEntities == null || !projectEntities.Any())
                {
                    Debug.WriteLine("Inga projekt hittades.");
                    return new List<ProjectModel>();
                }

                // Mappar varje ProjectEntity till en ProjectModel med hjälp av din factory
                return projectEntities
                    .Select(entity => ProjectFactory.CreateProjectModel(entity))
                    .ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fel vid hämtning av projekt: {ex.Message}");
                return new List<ProjectModel>();
            }
        }

        async Task<IEnumerable<ProjectDto>> IProjectService.GetAllProjectsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

