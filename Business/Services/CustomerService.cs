using System.Threading.Tasks;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class CustomerService : BaseService<CustomerModel, CustomerEntity, CustomerCreateDto>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository repository)
            : base(repository, CustomerFactory.ModelFromEntity, CustomerFactory.EntityFromModel, CustomerFactory.EntityFromDto)
        {
            _customerRepository = repository;
        }

        public async Task<CustomerModel> GetCustomerWithDetailsAsync(int id)
        {
            var entity = await _customerRepository.GetCustomerWithDetailsAsync(c => c.Id == id);
            return entity != null ? CustomerFactory.ModelFromEntity(entity) : null!;
        }
    }
}
