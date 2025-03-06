using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class Invoice
{
    [Key]
    public int Id { get; set; }

    public int JobId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateOfIssue { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalCost { get; set; }

    [StringLength(50)]
    public string PaymentStatus { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("Invoices")]
    public virtual RepairJob Job { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
