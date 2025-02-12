using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComputerRepairService.Models;

public partial class Payment
{
    [Key]
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public DateOnly PaymentDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

    public int PaymentMethod { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("Payments")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("PaymentMethod")]
    [InverseProperty("Payments")]
    public virtual PaymentMethod PaymentMethodNavigation { get; set; } = null!;
}
