namespace ComputerRepairService.Models.Dtos
{
    public class RepairJobDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string JobStatus { get; set; } = null!;
        public string? IssueDescription { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
