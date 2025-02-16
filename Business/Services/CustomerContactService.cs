using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services
{
    public class CustomerContactService(ICustomerContactRepository customerContactRepository) : ICustomerContactService
    {
        private readonly ICustomerContactRepository _customerContactRepository = customerContactRepository;

        public async Task<CustomerContactModel> CreateCustomerContactAsync(CustomerContactCreateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("All fields must be provided and cannot be empty or whitespace.");
            }

            try
            {
                var entity = CustomerContactFactory.CreateCustomerContactEntity(dto);
                var createdEntity = await _customerContactRepository.CreateAsync(entity);
                return CustomerContactFactory.CreateCustomerContactModel(createdEntity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating CustomerContact: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<CustomerContactModel>> GetAllCustomerContactsAsync()
        {
            try
            {
                var entities = await _customerContactRepository.GetAllWithDetailsAsync(query =>
                    query.Include(cc => cc.Customer));
                return entities.Select(e => CustomerContactFactory.CreateCustomerContactModel(e));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting all CustomerContacts: {ex.Message}");
                throw;
            }
        }

        public async Task<CustomerContactModel?> GetCustomerContactByIdAsync(int id)
        {
            try
            {
                var entity = await _customerContactRepository.GetOneWithDetailsAsync(
                    query => query.Include(cc => cc.Customer),
                    cc => cc.Id == id);

                return entity != null ? CustomerContactFactory.CreateCustomerContactModel(entity) : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting CustomerContact by id: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCustomerContactAsync(int id, CustomerContactUpdateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                throw new ArgumentException("All fields must be provided and cannot be empty or whitespace.");
            }

            try
            {
                var entity = await _customerContactRepository.GetOneAsync(cc => cc.Id == id);
                if (entity == null)
                    throw new Exception("CustomerContact not found");

                var updatedEntity = CustomerContactFactory.UpdateCustomerContactEntity(entity, dto);
                await _customerContactRepository.UpdateAsync(updatedEntity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating CustomerContact: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteCustomerContactAsync(int id)
        {
            try
            {
                var entity = await _customerContactRepository.GetOneAsync(cc => cc.Id == id);
                if (entity == null)
                    throw new Exception("CustomerContact not found");

                await _customerContactRepository.DeleteAsync(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting CustomerContact: {ex.Message}");
                throw;
            }
        }
    }
}
