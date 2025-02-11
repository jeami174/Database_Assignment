namespace Business.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public string CustomerType { get; set; } = string.Empty;
    }
}
