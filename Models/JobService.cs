using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComputerRepairService.Models;

public partial class JobService
{
    [Key]
    public int Id { get; set; }

    public int JobId { get; set; }

    public int ServiceId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Cost { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("JobServices")]
    public virtual RepairJob Job { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("JobServices")]
    public virtual Service Service { get; set; } = null!;
}
