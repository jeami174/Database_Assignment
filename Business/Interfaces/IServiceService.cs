using Business.Models;
using Data.Entities;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface IServiceService : IBaseService<ServiceModel, ServiceEntity, ServiceDto>
{
    Task<ServiceModel> GetServiceWithUnitAsync(int id);
}

