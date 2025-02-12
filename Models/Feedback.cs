using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComputerRepairService.Models;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    public int Id { get; set; }

    public int JobId { get; set; }

    [StringLength(255)]
    public string FeedbackText { get; set; } = null!;

    public double Rating { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("Feedbacks")]
    public virtual RepairJob Job { get; set; } = null!;
}
