using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Many;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels.Single
{
    public class JobServiceViewModel : BaseCreateViewModel<JobServiceService, JobServiceDto, JobService>
    {
        public ICommand SelectServiceCommand { get; set; }
        public int JobId
        {
            get => Model.JobId;
            set
            {
                if (Model.JobId != value)
                {
                    Model.JobId = value;
                    OnPropertyChanged(() => JobId);
                }
            }
        }
        public int ServiceId
        {
            get => Model.ServiceId;
            set
            {
                if (Model.ServiceId != value)
                {
                    Model.ServiceId = value;
                    OnPropertyChanged(() => ServiceId);
                    Cost = Service.GetServiceCostByServiceId(value);
                }
            }
        }
        public decimal Cost
        {
            get => Model.Cost;
            set
            {
                if (Model.Cost != value)
                {
                    Model.Cost = value;
                    OnPropertyChanged(() => Cost);
                }
            }
        }

        private string? _ServiceName { get; set; }
        public string? ServiceName
        {
            get => _ServiceName;
            set
            {
                if (_ServiceName != value)
                {
                    _ServiceName = value;
                    OnPropertyChanged(() => ServiceName);
                }
            }
        }
        private int _NumberOfActiveServices;
        public int NumberOfActiveServices
        {
            get => _NumberOfActiveServices;
            set
            {
                if (_NumberOfActiveServices != value)
                {
                    _NumberOfActiveServices = value;
                    OnPropertyChanged(() => NumberOfActiveServices);
                }
            }
        }
        public JobServiceViewModel() : base("Job Service")
        {
            ServiceName = "Select Service";
            NumberOfActiveServices = Service.InitializeNumberOfActiveServices();
            SelectServiceCommand = new BaseCommand(() => SelectService());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<int>>(this, (recipient, message) =>
            {
                if (message.WhoRequestedToSelect is AddNewRepairViewModel)
                {
                    JobId = message.SelectedObject;
                }
            });
            //this messenger listens for service id
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<ServiceDto>>(this, (recipient, message) => GetSelectedService(message));
        }
        public JobServiceViewModel(int id) : base("Job Service")
        {
            ServiceName = "Select Service";
            SelectServiceCommand = new BaseCommand(() => SelectService());
            //this messenger listens for id of repair job incoming
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<int>>(this, (recipient, message) =>
            {
                if (message.WhoRequestedToSelect is AddNewRepairViewModel)
                {
                    JobId = message.SelectedObject;
                }
            });
            //this messenger listens for service id
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<ServiceDto>>(this, (recipient, message) => GetSelectedService(message));

        }
        private void SelectService()
        {
            //Send message to show servicses and select one of them
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new ServicesWithCallbackViewModel(this)
            });
        }
        private void GetSelectedService(SelectedObjectMessage<ServiceDto> message)
        {
            if (message.WhoRequestedToSelect == this)
            {
                ServiceId = message.SelectedObject.Id;
                ServiceName = message.SelectedObject.ServiceName;
            }
        }
    }
}
