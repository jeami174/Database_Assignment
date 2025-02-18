using Business.Dtos;
using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceModel> CreateServiceAsync(ServiceCreateDto dto);
        Task<IEnumerable<ServiceModel>> GetAllServicesAsync();
        Task<ServiceModel?> GetServiceWithUnitAsync(int id);
        Task UpdateServiceAsync(int id, ServiceUpdateDto dto);
        Task DeleteServiceAsync(int id);
    }
}

