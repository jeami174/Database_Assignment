
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(RoleName), IsUnique = true)]
public class UserRoleEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string RoleName { get; set; } = null!;

    public ICollection<UserEntity> Users { get; set; } = [];
}
