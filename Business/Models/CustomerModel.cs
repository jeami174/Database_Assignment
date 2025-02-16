namespace Business.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public int CustomerTypeId { get; set; }

    public ICollection<CustomerContactModel> CustomerContacts { get; set; } = null!;

}
