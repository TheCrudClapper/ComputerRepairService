using ComputerRepairService.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Servicess
{
    public class BusinessService
    {
        protected DatabaseContext DatabaseContext { get; set; }
        public BusinessService()
        {
            DatabaseContext = new DatabaseContext();
        }
    }
}
