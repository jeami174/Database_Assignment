
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Interfaces;

namespace Data.Entities;

public class CustomerTypeEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string CustomerTypeName { get; set; } = null!;
}
