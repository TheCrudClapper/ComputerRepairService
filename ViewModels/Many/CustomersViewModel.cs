using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
namespace ComputerRepairService.ViewModels.Many
{
    public class CustomersViewModel : BaseManyViewModel<CustomerService, CustomerDto, Customer>
    {
        public bool HasNip
        {
            get => Service.HasNip;
            set
            {
                if (Service.HasNip != value)
                {
                    Service.HasNip = value;
                    OnPropertyChanged(() => HasNip);
                }
            }
        }
        public YesNoEnum HasPhoneNumber
        {
            get => Service.HasPhoneNumber;
            set
            {
                if (value != Service.HasPhoneNumber)
                {
                    Service.HasPhoneNumber = value;
                    OnPropertyChanged(() => HasPhoneNumber);
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
        public CustomersViewModel() : base("Customers")
        {
            HasNip = false;
            HasPhoneNumber = YesNoEnum.NoFilter;
        }
        protected override void ClearFilters()
        {
            HasNip = false;
            HasPhoneNumber = YesNoEnum.NoFilter;
            DateCreatedTo = null;
            DateCreatedFrom = null;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            Refresh();
        }

        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddCustomerViewModel()
            });
        }
        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddCustomerViewModel(SelectedModel.Id)
                });
            }

        }
    }
}
