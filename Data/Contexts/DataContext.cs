
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<UserEntity> Users { get; set; } = null!;
    public virtual DbSet<UserRoleEntity> UserRoles { get; set; } = null!;
    public virtual DbSet<CustomerEntity> Customers { get; set; } = null!;
    public virtual DbSet<CustomerTypeEntity> CustomerTypes { get; set; } = null!;
    public virtual DbSet<CustomerContactEntity> CustomerContacts { get; set; } = null!;
    public virtual DbSet<ProjectEntity> Projects { get; set; } = null!;
    public virtual DbSet<ServiceEntity> Services { get; set; } = null!;
    public virtual DbSet<StatusTypeEntity> StatusTypes { get; set; } = null!;
    public virtual DbSet<UnitEntity> Units { get; set; } = null!;
}
