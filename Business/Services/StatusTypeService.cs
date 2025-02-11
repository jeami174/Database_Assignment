using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class StatusTypeService : BaseService<StatusTypeModel, StatusTypeEntity, StatusTypeDto>, IStatusTypeService
    {
        private readonly IStatusTypeRepository _statusTypeRepository;

        public StatusTypeService(IStatusTypeRepository repository)
            : base(repository, StatusTypeFactory.ModelFromEntity, StatusTypeFactory.EntityFromModel, StatusTypeFactory.EntityFromDto)
        {
            _statusTypeRepository = repository;
        }

        public async Task<ICollection<StatusTypeModel>> GetAllStatusTypesAsync()
        {
            var entities = await _statusTypeRepository.GetAsync();
            return entities.Select(StatusTypeFactory.ModelFromEntity).ToList();
        }
    }
}
