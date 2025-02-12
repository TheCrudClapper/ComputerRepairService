using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ComputerRepairService.Models.BusinessObjects
{
    public class CardModel
    {
        public string Description { get; set; } = null!;
        public string Value { get; set; } = null!;
        public string Title { get; set; } = null!;
    }
}
