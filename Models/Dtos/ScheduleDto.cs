using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Dtos
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int EmployeeId { get; set; }

        public string? IssueDescription { get; set; }
        public string CustomerName { get; set; } = null!;
        public string EmployeeName { get; set; } = null!;
        public DateTime DateAssigned { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public DateTime? DateDeleted { get; set; }

    }
}
