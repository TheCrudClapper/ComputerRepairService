using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComputerRepairService.Models;

public partial class RepairJob
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [StringLength(255)]
    public string? IssueDescription { get; set; }

    public int JobStatus { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalCost { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RepairJobs")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Job")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [InverseProperty("Job")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Job")]
    public virtual ICollection<JobPart> JobParts { get; set; } = new List<JobPart>();

    [InverseProperty("Job")]
    public virtual ICollection<JobService> JobServices { get; set; } = new List<JobService>();

    [ForeignKey("JobStatus")]
    [InverseProperty("RepairJobs")]
    public virtual JobStatus JobStatusNavigation { get; set; } = null!;

    [InverseProperty("Job")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
