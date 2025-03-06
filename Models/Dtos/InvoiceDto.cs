namespace ComputerRepairService.Models.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Customer { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public string? IssueDescription { get; set; }
        public decimal TotalCost { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
