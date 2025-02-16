using Business.Dtos;
using Business.Factories;
using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<ServiceModel> CreateServiceAsync(ServiceCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.ServiceName))
            throw new ArgumentException("ServiceName cannot be empty or whitespace.", nameof(dto.ServiceName));

        dto.ServiceName = dto.ServiceName.Trim().ToLower();

        try
        {
            var serviceEntity = ServiceFactory.CreateServiceEntity(dto);
            var createdEntity = await _serviceRepository.CreateAsync(serviceEntity);
            return ServiceFactory.CreateServiceModel(createdEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating service: {ex.Message}");
            throw;
        }
    }
    public async Task<IEnumerable<ServiceModel>> GetAllServicesAsync()
    {
        try
        {
            var entities = await _serviceRepository.GetAllWithDetailsAsync(query => query.Include(s => s.Unit));
            return entities.Select(e => ServiceFactory.CreateServiceModel(e));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving services: {ex.Message}");
            throw;
        }
    }

    public async Task<ServiceModel?> GetServiceWithUnitAsync(int id)
    {
        try
        {
            var entity = await _serviceRepository.GetOneWithDetailsAsync(
                query => query.Include(s => s.Unit),
                s => s.Id == id);
            return entity != null ? ServiceFactory.CreateServiceModel(entity) : null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving service by id: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateServiceAsync(int id, ServiceUpdateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.ServiceName))
            throw new ArgumentException("ServiceName cannot be empty or whitespace.", nameof(dto.ServiceName));

        try
        {
            var entity = await _serviceRepository.GetOneAsync(s => s.Id == id);
            if (entity == null)
                throw new Exception("Service not found");

            var updatedEntity = ServiceFactory.UpdateServiceEntity(entity, dto);
            await _serviceRepository.UpdateAsync(updatedEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating service: {ex.Message}");
            throw;
        }
    }
    public async Task DeleteServiceAsync(int id)
    {
        try
        {
            var entity = await _serviceRepository.GetOneAsync(s => s.Id == id);
            if (entity == null)
                throw new Exception("Service not found");

            await _serviceRepository.DeleteAsync(entity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting service: {ex.Message}");
            throw;
        }
    }
}
