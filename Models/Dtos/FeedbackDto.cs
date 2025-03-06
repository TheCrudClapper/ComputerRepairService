namespace ComputerRepairService.Models.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Customer { get; set; } = null!;
        public string FeedbackText { get; set; } = null!;
        public string IssueDescription { get; set; } = null!;
        public double Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
