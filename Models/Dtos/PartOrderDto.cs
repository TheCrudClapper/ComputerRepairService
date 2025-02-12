using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Dtos
{
    public class PartOrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string  PartName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public decimal TotalCost { get; set; }
        public int QuantityOrdered { get; set; }
        public DateTime OrderDate { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
       
    }
}
