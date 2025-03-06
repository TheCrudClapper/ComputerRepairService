using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class Service
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string ServiceName { get; set; } = null!;

    [StringLength(255)]
    public string? ServiceDescription { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal BasePrice { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<JobService> JobServices { get; set; } = new List<JobService>();
}
