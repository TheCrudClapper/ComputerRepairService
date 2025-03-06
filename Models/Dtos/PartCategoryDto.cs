namespace ComputerRepairService.Models.Dtos
{
    public class PartCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
