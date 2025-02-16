
namespace Business.Dtos;

public class ServiceUpdateDto
{
    public string ServiceName { get; set; } = null!;
    public decimal PricePerHour { get; set; }

    public int UnitId { get; set; }
}
