using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using System.Collections.ObjectModel;
namespace ComputerRepairService.ViewModels.Single
{
    public class AddPartViewModel : BaseCreateViewModel<PartService, PartDto, Part>
    {
        public string PartName
        {
            get => Model.PartName;
            set
            {
                if (Model.PartName != value)
                {
                    Model.PartName = value;
                    OnPropertyChanged(() => PartName);
                }
            }
        }
        private ObservableCollection<ComboBoxDto> _PartCategories { get; set; }
        public ObservableCollection<ComboBoxDto> PartCategories
        {
            get => _PartCategories;
            set
            {
                if (_PartCategories != value)
                {
                    _PartCategories = value;
                    OnPropertyChanged(() => PartCategories);
                }
            }
        }
        public int PartCategoryId
        {
            get => Model.PartCategoryId;
            set
            {
                if (Model.PartCategoryId != value)
                {
                    Model.PartCategoryId = value;
                    OnPropertyChanged(() => PartCategoryId);
                }
            }
        }
        public int QuantityInStock
        {
            get => Model.QuantityInStock;
            set
            {
                if (Model.QuantityInStock != value)
                {
                    Model.QuantityInStock = value;
                    OnPropertyChanged(() => QuantityInStock);
                }
            }
        }
        public decimal UnitPrice
        {
            get => Model.UnitPrice;
            set
            {
                if (Model.UnitPrice != value)
                {
                    Model.UnitPrice = value;
                    OnPropertyChanged(() => UnitPrice);
                }
            }
        }
        public string? PartDescription
        {
            get => Model.PartDescription;
            set
            {
                if (Model.PartDescription != value)
                {
                    Model.PartDescription = value;
                    OnPropertyChanged(() => PartDescription);
                }
            }
        }
        private int _numberOfActiveParts;
        public int NumberOfActiveParts
        {
            get => _numberOfActiveParts;
            set
            {
                if (_numberOfActiveParts != value)
                {
                    _numberOfActiveParts = value;
                    OnPropertyChanged(() => NumberOfActiveParts);
                }
            }
        }
        public AddPartViewModel() : base("Part")
        {
            NumberOfActiveParts = Service.InitializeNumberOfActiveParts();
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            PartCategories = Service.InitializePartCategoriesComboBox();
        }
        public AddPartViewModel(int id) : base(id, "Part")
        {
            NumberOfActiveParts = Service.InitializeNumberOfActiveParts();
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            PartCategories = Service.InitializePartCategoriesComboBox();
        }
        public override void ClearInputFields()
        {
            PartName = string.Empty;
            PartDescription = string.Empty;
            QuantityInStock = 0;
            UnitPrice = 0;
            PartCategoryId = -1;
        }
    }
}
