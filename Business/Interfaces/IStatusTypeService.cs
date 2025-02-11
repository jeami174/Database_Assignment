using Business.Models;
using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStatusTypeService : IBaseService<StatusTypeModel, StatusTypeEntity, StatusTypeDto>
    {
        Task<ICollection<StatusTypeModel>> GetAllStatusTypesAsync();
    }
}
