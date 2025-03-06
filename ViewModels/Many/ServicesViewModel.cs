using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
namespace ComputerRepairService.ViewModels.Many
{
    public class ServicesViewModel : BaseManyViewModel<ServiceService, ServiceDto, Service>
    {
        public decimal? ServicePartCostFrom
        {
            get => Service.ServicePartCostFrom;
            set
            {
                if (Service.ServicePartCostFrom != value)
                {
                    Service.ServicePartCostFrom = value;
                    OnPropertyChanged(() => ServicePartCostFrom);
                }
            }
        }
        public decimal? ServicePartCostTo
        {
            get => Service.ServicePartCostTo;
            set
            {
                if (Service.ServicePartCostTo != value)
                {
                    Service.ServicePartCostTo = value;
                    OnPropertyChanged(() => ServicePartCostTo);
                }
            }
        }
        public ServicesViewModel() : base("Services")
        {
        }

        protected override void ClearFilters()
        {
            ServicePartCostFrom = null;
            ServicePartCostTo = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            SearchInput = null;
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
                    ViewModelToBeOpened = new AddServiceViewModel(SelectedModel.Id)
                });
            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddServiceViewModel()
            });
        }

    }
}
