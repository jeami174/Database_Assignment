using System.ComponentModel.DataAnnotations;
namespace Business.Dtos;

public class ProjectUpdateDto
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }

    public int StatusId { get; set; }
    public int UserId { get; set; }
}
