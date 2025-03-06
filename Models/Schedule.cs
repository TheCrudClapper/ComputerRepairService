using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class Schedule
{
    [Key]
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int JobId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateAssigned { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Schedules")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("JobId")]
    [InverseProperty("Schedules")]
    public virtual RepairJob Job { get; set; } = null!;
}
