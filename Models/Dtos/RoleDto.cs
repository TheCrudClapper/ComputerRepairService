using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Dtos
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
