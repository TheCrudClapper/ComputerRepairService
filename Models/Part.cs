using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class Part
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string PartName { get; set; } = null!;

    public int PartCategoryId { get; set; }

    [StringLength(255)]
    public string? PartDescription { get; set; }

    public int QuantityInStock { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [InverseProperty("Part")]
    public virtual ICollection<JobPart> JobParts { get; set; } = new List<JobPart>();

    [ForeignKey("PartCategoryId")]
    [InverseProperty("Parts")]
    public virtual PartCategory PartCategory { get; set; } = null!;

    [InverseProperty("Part")]
    public virtual ICollection<PartOrder> PartOrders { get; set; } = new List<PartOrder>();
}
