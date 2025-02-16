namespace Business.Dtos;
public class ProjectDto
{
    /// <summary>
    /// This DTO is used for the user interface on the main page where all projects are displayed. 
    /// The reason for creating a separate DTO for the UI is to avoid loading data from all tables unless necessary
    /// </summary>
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }
}
