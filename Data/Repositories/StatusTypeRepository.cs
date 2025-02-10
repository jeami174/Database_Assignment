using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context) : BaseRepository<StatusTypeEntity>(context), IStatusTypeRepository
{
}
