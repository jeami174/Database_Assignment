﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(ServiceName), IsUnique = true)]
public class ServiceEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string ServiceName { get; set; } = null!;

    [Required]
    public decimal PricePerHour{ get; set; }

    [ForeignKey("Unit")]
    public int UnitId { get; set; }
    public UnitEntity Unit { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];

}
