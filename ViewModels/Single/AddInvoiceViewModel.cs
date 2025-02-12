using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ComputerRepairService.Helpers;
using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.ViewModels.Many;

namespace ComputerRepairService.ViewModels.Single
{
    public class AddInvoiceViewModel : BaseCreateViewModel<InvoiceService, InvoiceDto, Invoice>
    {
        private JobServiceService _JobServiceService
        {
            get => Service.JobServiceService;
        }
        private JobPartService _JobPartService
        {
            get => Service.JobPartService;
        }
        public ICommand SelectRepairJobCommand { get; set; }
        public int RepairJobId
        {
            get => Model.JobId;
            set
            {
                if (Model.JobId != value)
                {
                    Model.JobId = value;
                    OnPropertyChanged(() => RepairJobId);
                    RefreshJobParts();
                    RefreshJobServices();
                    InitializeInvoiceCustomer();
                }
            }
        }
        public DateTime DateOfIssue
        {
            get => Model.DateOfIssue;
            set
            {
                if (DateOfIssue != value)
                {
                    Model.DateOfIssue = value;
                    OnPropertyChanged(() => DateOfIssue);
                }

            }
        }
        public string PaymentStatus
        {
            get => Model.PaymentStatus;
            set
            {
                if (Model.PaymentStatus != value)
                {
                    Model.PaymentStatus = value;
                    OnPropertyChanged(() => PaymentStatus);
                }
            }
        }
        //property that helps to divide total cost from database and the one calculated based on prices and services
        //attached to specific repair job
        private decimal _RecalculatedTotalCost { get; set; }
        public decimal RecaluculatedTotalCost
        {
            get => _RecalculatedTotalCost;
            set
            {
                if (_RecalculatedTotalCost != value)
                {
                    _RecalculatedTotalCost = value;
                    OnPropertyChanged(() => RecaluculatedTotalCost);
                }
            }
        }

        public decimal TotalCost
        {
            //when to total cost from database is not zero then total cost from database is displayed
            //when is zero then price caluclated by services and prices is set
            get => Model.TotalCost != 0 ? Model.TotalCost : RecaluculatedTotalCost;
            set
            {
                if (Model.TotalCost != value)
                {
                    Model.TotalCost = value;
                    OnPropertyChanged(() => TotalCost);
                }
            }
        }
        private decimal _TotalServicesCost { get; set; }
        public decimal TotalServicesCost
        {
            get => _TotalServicesCost;
            set
            {
                if (_TotalServicesCost != value)
                {
                    _TotalServicesCost = value;
                    OnPropertyChanged(() => TotalServicesCost);
                }
            }
        }
        private decimal? _TotalPartsCost { get; set; }
        public decimal? TotalPartsCost
        {
            get => _TotalPartsCost;
            set
            {
                if (_TotalPartsCost != value)
                {
                    _TotalPartsCost = value;
                    OnPropertyChanged(() => TotalPartsCost);
                }
            }
        }
        private CustomerInvoiceDto _InvoiceCustomer { get; set; }
        public CustomerInvoiceDto InvoiceCustomer
        {
            get => _InvoiceCustomer;
            set
            {
                if (_InvoiceCustomer != value)
                {
                    _InvoiceCustomer = value;
                    OnPropertyChanged(() => InvoiceCustomer);
                }
            }
        }
        public AddInvoiceViewModel() : base("Invoice")
        {
            PaymentStatuses = Service.InitializePaymentStatuses();
            DateOfIssue = DateTime.Now;
            SelectRepairJobCommand = new BaseCommand(() => SelectCustomer());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<RepairJobDto>>(this, (recipient, message) => GetSelectedRepairJob(message));
        }
        public AddInvoiceViewModel(int id) : base(id, "Invoice")
        {
            PaymentStatuses = Service.InitializePaymentStatuses();
            SelectRepairJobCommand = new BaseCommand(() => SelectCustomer());
            //after selecting something to edit we need to once again fire those methods to get all data for specific invoice
            RefreshJobParts();
            RefreshJobServices();
            //for displaying his infor
            InitializeInvoiceCustomer();
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<RepairJobDto>>(this, (recipient, message) => GetSelectedRepairJob(message));
        }
        private ObservableCollection<string> _PaymentStatuses { get; set; }
        public ObservableCollection<string> PaymentStatuses
        {
            get => _PaymentStatuses;
            set
            {
                if (_PaymentStatuses != value)
                {
                    _PaymentStatuses = value;
                    OnPropertyChanged(() => PaymentStatuses);
                }
            }
        }
        private ObservableCollection<JobPartDto> _JobParts { get; set; }
        public ObservableCollection<JobPartDto> JobParts
        {
            get => _JobParts;
            set
            {
                if (_JobParts != value)
                {
                    _JobParts = value;
                    OnPropertyChanged(() => JobParts);
                }

            }
        }
        private ObservableCollection<JobServiceDto> _JobServices { get; set; }
        public ObservableCollection<JobServiceDto> JobServices
        {
            get => _JobServices;
            set
            {
                if (_JobServices != value)
                {
                    _JobServices = value;
                    OnPropertyChanged(() => JobServices);
                }

            }
        }
        public void InitializeInvoiceCustomer()
        {
            if (RepairJobId != default)
            {
                InvoiceCustomer = Service.GetCustomerDetailsById(RepairJobId);
            }
        }
        private void RefreshJobServices()
        {
            if (RepairJobId != default)
            {
                JobServices = new ObservableCollection<JobServiceDto>(_JobServiceService.GetModelsBySelectedRepairID(RepairJobId));
            }
            else
            {
                JobServices = new ObservableCollection<JobServiceDto>();
            }
            CalcualteTotalCosts();
        }
        private void RefreshJobParts()
        {
            if (RepairJobId != default)
            {
                JobParts = new ObservableCollection<JobPartDto>(_JobPartService.GetModelsBySelectedRepairID(RepairJobId));
            }
            else
            {
                JobParts = new ObservableCollection<JobPartDto>();
            }
            CalcualteTotalCosts();
        }
        private void CalcualteTotalCosts()
        {
            if (RepairJobId != default)
            {
                var (totalCost, totalPartsCost, totalServicesCost) = Service.CalculateTotalCosts(RepairJobId);
                RecaluculatedTotalCost = totalCost;
                TotalPartsCost = totalPartsCost;
                TotalServicesCost = totalServicesCost;
            }
            else
            {
                TotalCost = 0;
                TotalPartsCost = 0;
                TotalServicesCost = 0;
            }
        }
        private void SelectCustomer()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new RepairsWithCallbackViewModel(this)
            });

        }
        private void GetSelectedRepairJob(SelectedObjectMessage<RepairJobDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                RepairJobId = message.SelectedObject.Id;
            }
        }
    }

}
