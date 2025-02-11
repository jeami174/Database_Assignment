using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(StatusTypeName), IsUnique = true)]
public class StatusTypeEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string StatusTypeName { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}
