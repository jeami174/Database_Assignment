
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
namespace Data.Entities;

public class CustomerContactEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Email { get; set; } = null!;

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;
}
