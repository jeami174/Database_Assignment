using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Threading.Tasks;

namespace Business.Interfaces;

public interface ICustomerService : IBaseService<CustomerModel, CustomerEntity, CustomerCreateDto>
{
    Task<CustomerModel> GetCustomerWithDetailsAsync(int id);
}
