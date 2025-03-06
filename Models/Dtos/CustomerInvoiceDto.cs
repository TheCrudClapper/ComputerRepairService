namespace ComputerRepairService.Models.Dtos
{
    public class CustomerInvoiceDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Nip { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? Place { get; set; }
    }
}
