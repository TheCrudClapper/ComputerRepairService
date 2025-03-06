using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class JobStatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string StatusName { get; set; } = null!;

    [StringLength(255)]
    public string? StatusDescription { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [InverseProperty("JobStatusNavigation")]
    public virtual ICollection<RepairJob> RepairJobs { get; set; } = new List<RepairJob>();
}
