namespace ComputerRepairService.Models.Dtos
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Nip { get; set; }
        public string? Email { get; set; }
        public string? Place { get; set; }
        public string? Street { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public string? HouseNumber { get; set; }
    }
}
