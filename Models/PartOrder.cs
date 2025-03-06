using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class PartOrder
{
    [Key]
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int PartId { get; set; }

    public int QuantityOrdered { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("PartId")]
    [InverseProperty("PartOrders")]
    public virtual Part Part { get; set; } = null!;

    [ForeignKey("SupplierId")]
    [InverseProperty("PartOrders")]
    public virtual Supplier Supplier { get; set; } = null!;
}
