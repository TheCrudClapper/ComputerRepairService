namespace ComputerRepairService.Models.Dtos
{
    public class PartDto
    {
        public int Id { get; set; }
        public string PartName { get; set; } = null!;
        public string? PartDescription { get; set; }
        public string? PartCategory { get; set; }
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public bool isActive { get; set; }
    }
}
