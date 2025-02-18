using Business.Dtos;
using Business.Factories;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

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
            return ProjectFactory.CreateProjectDetailModel(createdEntity);
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

            entity = ProjectFactory.UpdateProjectEntity(entity, dto);
            var updatedEntity = await _projectRepository.UpdateAsync(entity);
            if (updatedEntity == null)
                throw new Exception("Error updating project");

            await transaction.CommitAsync();
            return ProjectFactory.CreateProjectDetailModel(entity);
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
        var entity = await _projectRepository.GetProjectWithDetailsAsync(id);
        return entity != null ? ProjectFactory.CreateProjectDetailModel(entity) : null;
    }

    public async Task<IEnumerable<ProjectModel>> ReadAllWithoutDetailsAsync()
    {
        try
        {
            // Hämtar entiteter med minimala Include-satser (Customer, Status och User)
            var entities = await _projectRepository.GetAllWithDetailsAsync(query =>
                query.Include(p => p.Customer)
                     .Include(p => p.Status)
                     .Include(p => p.User));
            return entities.Select(ProjectFactory.CreateProjectOverviewModel).ToList();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error reading projects: {ex.Message}");
            return new List<ProjectModel>();
        }
    }
}