namespace ComputerRepairService.Models.Dtos
{
    public class JobStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = null!;
        public string StatusDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
