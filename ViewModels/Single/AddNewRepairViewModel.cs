using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Many;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels.Single
{
    public class AddNewRepairViewModel : BaseCreateViewModel<RepairJobService, RepairJobDto, RepairJob>
    {
        private JobServiceService _JobServiceService
        {
            get => Service._JobServiceService;
        }
        private JobPartService _JobPartService
        {
            get => Service._JobPartService;
        }
        public ICommand AddJobPartCommand { get; set; }
        public ICommand AddJobServiceCommand { get; set; }
        public ICommand DeleteJobServiceCommand { get; set; }
        public ICommand DeleteJobPartCommand { get; set; }
        public ICommand RefreshJobServicesCommand {  get; set; }
        public ICommand RefreshJobPartsCommand { get; set; }
        public ICommand SelectRepairJobCommand { get; set; }
        public ICommand SelectCustomerCommand { get; set; }
        public AddNewRepairViewModel() : base("Repair")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            RefreshJobServicesCommand = new BaseCommand(() => RefreshJobServices());
            RefreshJobPartsCommand = new BaseCommand(() => RefreshJobParts());
            DeleteJobPartCommand = new BaseCommand(() => DeleteJobPart());
            DeleteJobServiceCommand = new BaseCommand(() => DeleteJobService());
            SelectRepairJobCommand = new BaseCommand(() => SelectRepairJob());
            SelectCustomerCommand = new BaseCommand(() => SelectCustomer());
            AddJobPartCommand = new BaseCommand(() => OpenAddJobPart());
            AddJobServiceCommand = new BaseCommand(() => OpenAddJobService());
            JobStatuses = Service.InitializeJobStatusesComboBox();
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<RepairJobDto>>(this, (recipient, message) => GetSelectedRepairJob(message));
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CustomerDto>>(this, (recipient, message) => GetSelectedCustomer(message));
        }
        public AddNewRepairViewModel(int id) : base(id, "Repair")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            RefreshJobServicesCommand = new BaseCommand(() => RefreshJobServices());
            RefreshJobPartsCommand = new BaseCommand(() => RefreshJobParts());
            DeleteJobPartCommand = new BaseCommand(() => DeleteJobPart());
            DeleteJobServiceCommand = new BaseCommand(() => DeleteJobService());
            SelectRepairJobCommand = new BaseCommand(() => SelectRepairJob());
            SelectCustomerCommand = new BaseCommand(() => SelectCustomer());
            AddJobPartCommand = new BaseCommand(() => OpenAddJobPart());
            AddJobServiceCommand = new BaseCommand(() => OpenAddJobService());
            JobStatuses = Service.InitializeJobStatusesComboBox();
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<RepairJobDto>>(this, (recipient, message) => GetSelectedRepairJob(message));
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<CustomerDto>>(this, (recipient, message) => GetSelectedCustomer(message));
            CustomerName = Service.GetCustomerNameById(CustomerId);
            RefreshJobServices();
            RefreshJobParts();
        }
        public int CustomerId
        {
            get => Model.CustomerId;
            set
            {
                if (Model.CustomerId != value)
                {
                    Model.CustomerId = value;
                    OnPropertyChanged(() => CustomerId);
                }
            }
        }
        private int? _SelectedRepairJobId;
        public int? SelectedRepairJobId
        {
            get => _SelectedRepairJobId;
            set
            {
                if (_SelectedRepairJobId != value)
                {
                    _SelectedRepairJobId = value;
                    OnPropertyChanged(() => SelectedRepairJobId);
                    RefreshJobServices();
                    RefreshJobParts();
                }
            }
        }
        private string _CustomerName { get; set; }
        public string CustomerName
        {
            get => _CustomerName;
            set
            {
                if(_CustomerName != value)
                {
                    _CustomerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }
        public string? IssueDescription
        {
            get => Model.IssueDescription;
            set
            {
                if (Model.IssueDescription != value)
                {
                    Model.IssueDescription = value;
                    OnPropertyChanged(() => IssueDescription);
                }
            }
        }
        public int JobStatusId
        {
            get => Model.JobStatus;
            set
            {
                if (Model.JobStatus != value)
                {
                    Model.JobStatus = value;
                    OnPropertyChanged(() => JobStatusId);
                }
            }
        }
        private ObservableCollection<ComboBoxDto> _JobStatuses { get; set; }
        public ObservableCollection<ComboBoxDto> JobStatuses
        {   
            get => _JobStatuses;
            set
            {
                if (JobStatuses != value)
                {
                    _JobStatuses = value;
                    OnPropertyChanged(() => JobStatuses);
                }
            }
        }
        private ObservableCollection<JobServiceDto> _JobServices { get; set; }
        public ObservableCollection<JobServiceDto> JobServices
        {
            get => _JobServices;
            set
            {
                if(_JobServices != value)
                {
                    _JobServices = value;
                    OnPropertyChanged(() => JobServices);
                }
            }
        }
        private ObservableCollection<JobPartDto> _JobParts { get; set; }
        public ObservableCollection<JobPartDto> JobParts
        {
            get => _JobParts;
            set
            {
                if(_JobParts != value)
                {
                    _JobParts = value;
                    OnPropertyChanged(() => JobParts);
                }
            }
        }
        public void RefreshJobServices()
        {
            if (SelectedRepairJobId.HasValue)
            {
                JobServices = new ObservableCollection<JobServiceDto>(
                    _JobServiceService.GetModelsBySelectedRepairID(SelectedRepairJobId.Value)
                );
            }
            else
            {
                JobServices = new ObservableCollection<JobServiceDto>();
            }
            calculateTotalCosts();
        }
        public void RefreshJobParts()
        {
            if (SelectedRepairJobId.HasValue)
            {
                JobParts = new ObservableCollection<JobPartDto>(
                    _JobPartService.GetModelsBySelectedRepairID(SelectedRepairJobId.Value)
                );
            }
            else
            {
                JobParts = new ObservableCollection<JobPartDto>();
            }
            calculateTotalCosts();
        }
        //property that holds total service cost value
        private decimal? _TotalServicesCost { get; set; }
        public decimal? TotalServicesCost
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

        //propterty that holds total parts cost
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
        //property that holds total cost of parts nad services combined
        private decimal? _TotalCost { get; set; }
        public decimal? TotalCost
        {
            get => _TotalCost;
            set
            {
                if (_TotalCost != value)
                {
                    _TotalCost = value;
                    OnPropertyChanged(() => TotalCost);
                }
            }
        }
        private JobPartDto _SelectedJobPartDto { get; set; }
        public JobPartDto SelectedJobPartDto
        {
            get => _SelectedJobPartDto;
            set
            {
                if (_SelectedJobPartDto != value)
                {
                    _SelectedJobPartDto = value;
                    OnPropertyChanged(() => SelectedJobPartDto);
                }
            }
        }
        private JobServiceDto _SelectedJobServiceDto { get; set; }
        public JobServiceDto SelectedJobServiceDto
        {
            get => _SelectedJobServiceDto;
            set
            {
                if (_SelectedJobServiceDto != value)
                {
                    _SelectedJobServiceDto = value;
                    OnPropertyChanged(() => SelectedJobServiceDto);
                }
            }
        }
        //sending message to open new window in order to select repair job
        private void SelectRepairJob()
        {
            //send message to show repair job and select one of them
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new RepairsWithCallbackViewModel(this)
            });
        }
        //method that gets selected repair job and assigns values to fields
        private void GetSelectedRepairJob(SelectedObjectMessage<RepairJobDto> message)
        {
            if(message.WhoRequestedToSelect == this)
            {
                SelectedRepairJobId = message.SelectedObject.Id;
            }
        }
        //sending message to open new window in order to select customer
        private void SelectCustomer()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new CustomersWithCallbackViewModel(this)
            });

        }
        //function that gets selected customer and assigns values to fields
        private void GetSelectedCustomer(SelectedObjectMessage<CustomerDto> message)
        {
            if(message.WhoRequestedToSelect == this)
            {
                CustomerId = message.SelectedObject.Id;
                //this only works when we add new customer
                CustomerName = message.SelectedObject.FirstName + " " + message.SelectedObject.Surname;
            }
        }
        //function that calcuates total costs displayed in view 
        //using methods defined on service
        public void calculateTotalCosts()
        {
            if (SelectedRepairJobId.HasValue)
            {
                var (servicesCost, partsCost, totalCost)= Service.CalculateTotalCosts(SelectedRepairJobId.Value);
                TotalCost = totalCost;
                TotalPartsCost = partsCost;
                TotalServicesCost = servicesCost;
            }
            else
            {
                TotalPartsCost = 0;
                TotalServicesCost = 0;
                TotalCost = 0;
            }

        }
        public void DeleteJobPart()
        {
            if(SelectedJobPartDto != null)
            {
                _JobPartService.RevertUsedPart(SelectedJobPartDto.PartId, SelectedJobPartDto.QuantityUsed);
                _JobPartService.DeleteModel(SelectedJobPartDto);
                JobParts.Remove(SelectedJobPartDto);
                //reverting changes made while adding new job part
                calculateTotalCosts();
            }
        }
        public void DeleteJobService()
        {
            if(SelectedJobServiceDto != null)
            {
                _JobServiceService.DeleteModel(SelectedJobServiceDto);
                JobServices.Remove(SelectedJobServiceDto);
                calculateTotalCosts();
            }
        }
        public override void ClearInputFields()
        {
            JobStatusId = -1;
            IssueDescription = String.Empty;
        }
        //sending a message to open add job service view
        private void OpenAddJobService()
        {
            if (SelectedRepairJobId.HasValue)
            {
                if (SelectedRepairJobId.HasValue)
                {
                    //Sending messaget to open new view to mainwindowvm
                    WeakReferenceMessenger.Default.Send(new OpenViewMessage()
                    {
                        Sender = this,
                        ViewModelToBeOpened = new JobServiceViewModel()
                    });
                    //sending message with id of selected repair job to later add to this job part
                    WeakReferenceMessenger.Default.Send(new SelectedObjectMessage<int>(this, SelectedRepairJobId.Value));
                }
            }
            else
            {
                MessageBox.Show("Please, first choose repair !", "Select Repair!");
            }

        }
        //sending a message to open add job part view
        private void OpenAddJobPart()
        {
            if (SelectedRepairJobId.HasValue)
            {
                if (SelectedRepairJobId.HasValue)
                {
                    //Sending messaget to open new view to mainwindowvm
                    WeakReferenceMessenger.Default.Send(new OpenViewMessage()
                    {
                        Sender = this,
                        ViewModelToBeOpened = new JobPartViewModel()
                    });
                    //sending message with id of selected repair job to later add to this job part
                    WeakReferenceMessenger.Default.Send(new SelectedObjectMessage<int>(this, SelectedRepairJobId.Value));
                }
            }
            else
            {
                MessageBox.Show("Please, first choose repair !", "Select Repair!");
            }
        }
   }
}
