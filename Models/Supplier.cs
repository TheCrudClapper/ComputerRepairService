using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ComputerRepairService.Models;

public partial class Supplier
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [Column("NIP")]
    [StringLength(20)]
    public string? Nip { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    public int AddressId { get; set; }

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Suppliers")]
    public virtual Address Address { get; set; } = null!;

    [InverseProperty("Supplier")]
    public virtual ICollection<PartOrder> PartOrders { get; set; } = new List<PartOrder>();
}
