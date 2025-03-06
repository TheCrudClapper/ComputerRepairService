using ComputerRepairService.Helpers;

namespace ComputerRepairService.Models.Dtos
{
    public class YesComboBoxItemDto
    {
        public YesNoEnum SelectedOption { get; set; }
        public string OptionName { get; set; } = default!;
    }
}
