using System.Threading.Tasks;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class ServiceService : BaseService<ServiceModel, ServiceEntity, ServiceDto>, IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository repository)
            : base(repository, ServiceFactory.ModelFromEntity, ServiceFactory.EntityFromModel, ServiceFactory.EntityFromDto)
        {
            _serviceRepository = repository;
        }

        public async Task<ServiceModel> GetServiceWithUnitAsync(int id)
        {
            var entity = await _serviceRepository.GetServiceWithUnitAsync(s => s.Id == id);
            return entity != null ? ServiceFactory.ModelFromEntity(entity) : null!;
        }
    }
}
