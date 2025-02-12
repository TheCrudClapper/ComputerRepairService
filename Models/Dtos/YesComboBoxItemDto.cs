using ComputerRepairService.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Dtos
{
    public class YesComboBoxItemDto
    {
        public YesNoEnum SelectedOption { get; set; }
        public string OptionName { get; set; } = default!;
    }
}
