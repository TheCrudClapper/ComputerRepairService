using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Dtos
{
    public class JobPartDto
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public string PartName { get; set; } = null!;
        public string PartCategory { get; set; } = null!;
        public int QuantityUsed { get; set; }
        public int RepairJobId { get; set; }
        public decimal? Cost { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
