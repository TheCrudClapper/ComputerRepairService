using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
namespace ComputerRepairService.ViewModels.Many
{
    public class OrdersViewModels : BaseManyViewModel<PartOrderService, PartOrderDto, PartOrder>
    {
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
        public decimal? UnitPriceFrom
        {
            get => Service.UnitPriceFrom;
            set
            {
                if (Service.UnitPriceFrom != value)
                {
                    Service.UnitPriceFrom = value;
                    OnPropertyChanged(() => UnitPriceFrom);
                }
            }
        }
        public decimal? UnitPriceTo
        {
            get => Service.UnitPriceTo;
            set
            {
                if (Service.UnitPriceTo != value)
                {
                    Service.UnitPriceTo = value;
                    OnPropertyChanged(() => UnitPriceTo);
                }
            }
        }
        public bool HasDeliveryDate
        {
            get => Service.HasDeliveryDate;
            set
            {
                if (Service.HasDeliveryDate != value)
                {
                    Service.HasDeliveryDate = value;
                    OnPropertyChanged(() => HasDeliveryDate);
                }
            }
        }
        public OrdersViewModels() : base("Orders")
        {

        }

        protected override void ClearFilters()
        {
            DateCreatedFrom = null;
            DateCreatedTo = null;
            UnitPriceFrom = null;
            UnitPriceTo = null;
            HasDeliveryDate = false;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            SearchInput = null;
            Refresh();
        }
        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddOrderViewModel(SelectedModel.Id)
                });
            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddOrderViewModel()
            });
        }

    }
}
