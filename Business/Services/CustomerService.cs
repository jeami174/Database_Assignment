using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerModel> CreateCustomerAsync(CustomerCreateDto dto)
    {

        if (string.IsNullOrWhiteSpace(dto.CustomerName))
            throw new ArgumentException("CustomerName cannot be empty or whitespace.", nameof(dto.CustomerName));

        dto.CustomerName = dto.CustomerName.ToLower();

        bool exists = await _customerRepository.ExistsAsync(x => x.CustomerName.ToLower() == dto.CustomerName);
        if (exists)
            throw new Exception("A customer with the same name already exists.");

        try
        {
            var customerEntity = CustomerFactory.CreateCustomerEntity(dto);
            var createdEntity = await _customerRepository.CreateAsync(customerEntity);
            return CustomerFactory.CreateCustomerModel(createdEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity :: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetCustomersWithDetailsAsync();
        return entities.Select(e => CustomerFactory.CreateCustomerModel(e));
    }

    public async Task<CustomerModel?> GetCustomerWithDetailsAsync(int id)
    {
        var entity = await _customerRepository.GetCustomerWithDetailsAsync(c => c.Id == id);
        return entity != null ? CustomerFactory.CreateCustomerModel(entity) : null;
    }

    public async Task UpdateCustomerAsync(int id, CustomerUpdateDto dto)
    {
        var entity = await _customerRepository.GetOneAsync(c => c.Id == id);
        if (entity == null)
        {
            throw new Exception("Customer not found");
        }

        var updatedEntity = CustomerFactory.CreateUpdatedEntity(dto, entity);
        await _customerRepository.UpdateAsync(updatedEntity);
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var entity = await _customerRepository.GetOneAsync(c => c.Id == id);
        if (entity == null)
        {
            throw new Exception("Customer not found");
        }
        await _customerRepository.DeleteAsync(entity);
    }
}

