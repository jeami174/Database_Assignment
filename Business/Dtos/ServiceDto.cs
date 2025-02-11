namespace Business.Models;

public class ServiceDto
{
    public string ServiceName { get; set; } = string.Empty;
    public decimal PricePerHour { get; set; }
    public int UnitId { get; set; }
}

