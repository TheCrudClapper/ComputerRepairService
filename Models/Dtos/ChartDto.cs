using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Dtos
{
    public class ChartDto
    {
        public string Label { get; set; } = null!;
        public double Value { get; set; }
    }
}
