using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;

namespace ComputerRepairService.ViewModels.Many
{
    public class RepairsViewModel : BaseManyViewModel<RepairJobService, RepairJobDto, RepairJob>
    {

        public decimal? TotalCostFrom
        {
            get => Service.TotalCostFrom;
            set
            {
                if (Service.TotalCostFrom != value)
                {
                    Service.TotalCostFrom = value;
                    OnPropertyChanged(() => TotalCostFrom);
                }
            }
        }
        public decimal? TotalCostTo
        {
            get => Service.TotalCostTo;
            set
            {
                if (Service.TotalCostTo != value)
                {
                    Service.TotalCostTo = value;
                    OnPropertyChanged(() => TotalCostTo);
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

        public RepairsViewModel() : base("Repairs")
        {

        }

        protected override void ClearFilters()
        {
            TotalCostFrom = null;
            TotalCostTo = null;
            DateCreatedFrom = null;
            DateCreatedTo = null;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }

        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddNewRepairViewModel(SelectedModel.Id)
                });
            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddNewRepairViewModel()
            });
        }
    }
}
