using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComputerRepairService.Models;

public partial class JobPart
{
    [Key]
    public int Id { get; set; }

    public int JobId { get; set; }

    public int PartId { get; set; }

    public int QuantityUsed { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Cost { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("JobParts")]
    public virtual RepairJob Job { get; set; } = null!;

    [ForeignKey("PartId")]
    [InverseProperty("JobParts")]
    public virtual Part Part { get; set; } = null!;
}
