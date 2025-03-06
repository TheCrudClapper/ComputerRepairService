namespace ComputerRepairService.Models.Dtos
{
    public class JobServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public int RepairJobId { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
