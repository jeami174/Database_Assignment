using Business.Dtos;
using Business.Models;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<CustomerModel> CreateCustomerAsync(CustomerCreateDto dto);
    Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
    Task<CustomerModel?> GetCustomerWithDetailsAsync(int id);
    Task UpdateCustomerAsync(int id, CustomerUpdateDto dto);
    Task DeleteCustomerAsync(int id);
}