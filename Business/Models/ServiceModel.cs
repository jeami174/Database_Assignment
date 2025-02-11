namespace Business.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }

        public string UnitName { get; set; } = string.Empty;
    }
}
