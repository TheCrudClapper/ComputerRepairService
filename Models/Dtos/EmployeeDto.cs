namespace ComputerRepairService.Models.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Position { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateOnly HireDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
