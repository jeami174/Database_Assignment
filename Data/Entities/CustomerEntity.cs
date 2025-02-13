﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Business.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace Data.Entities;

public class CustomerEntity : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string CustomerName { get; set; } = null!;

    [ForeignKey("CustomerType")]
    public int CustomerTypeId { get; set; }
    public CustomerTypeEntity CustomerType { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
    public ICollection<CustomerContactEntity> Contacts { get; set; } = [];


}
