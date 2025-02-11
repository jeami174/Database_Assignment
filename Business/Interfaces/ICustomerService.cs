using Business.Models;
using Data.Entities;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface ICustomerService : IBaseService<CustomerModel, CustomerEntity, CustomerDto>
{
    Task<CustomerModel> GetCustomerWithDetailsAsync(int id);
}
