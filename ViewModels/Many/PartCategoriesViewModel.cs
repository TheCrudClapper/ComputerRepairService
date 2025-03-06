using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;

namespace ComputerRepairService.ViewModels.Many
{
    public class PartCategoriesViewModel : BaseManyViewModel<PartCategoryService, PartCategoryDto, PartCategory>
    {
        public bool HasDescription
        {
            get => Service.HasDescription;
            set
            {
                if (Service.HasDescription != value)
                {
                    Service.HasDescription = value;
                    OnPropertyChanged(() => HasDescription);
                }
            }
        }
        public DateTime? DateCreatedFrom
        {
            get => Service.DateCreatedFrom;
            set
            {
                if (Service.DateCreatedFrom != value)
                {
                    Service.DateCreatedFrom = value;
                    OnPropertyChanged(() => DateCreatedFrom);
                }
            }
        }

        public DateTime? DateCreatedTo
        {
            get => Service.DateCreatedTo;
            set
            {
                if (Service.DateCreatedTo != value)
                {
                    Service.DateCreatedTo = value;
                    OnPropertyChanged(() => DateCreatedTo);
                }
            }
        }
        public PartCategoriesViewModel() : base("Part Categories")
        {
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
        }

        protected override void ClearFilters()
        {
            DateCreatedFrom = null;
            DateCreatedTo = null;
            HasDescription = false;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            Refresh();
        }
        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddPartCategoryViewModel(SelectedModel.Id)
                });
            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddPartCategoryViewModel()
            });
        }
    }
}
