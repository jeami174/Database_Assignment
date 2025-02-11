using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(UnitName), IsUnique = true)]
public class UnitEntity : IEntity
{   
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string UnitName { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];

}
