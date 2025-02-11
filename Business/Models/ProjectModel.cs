using System;

namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? FullDescription { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Status { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;

    public decimal ServicePrice { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string? CustomerContact { get; set; }
}

