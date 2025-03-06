using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerRepairService.Models;

public partial class Address
{
    [Key]
    public int Id { get; set; }

    [StringLength(128)]
    public string? Place { get; set; }

    [StringLength(128)]
    public string? Street { get; set; }

    [StringLength(16)]
    public string? HouseNumber { get; set; }

    [StringLength(100)]
    public string PostalCity { get; set; } = null!;

    [StringLength(32)]
    public string PostalCode { get; set; } = null!;

    [StringLength(100)]
    public string Country { get; set; } = null!;

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateEdited { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateDeleted { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Address")]
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
