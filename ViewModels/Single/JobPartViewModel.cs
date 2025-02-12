using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using System.Windows.Input;
using ComputerRepairService.ViewModels.Many;

namespace ComputerRepairService.ViewModels.Single
{
    public class JobPartViewModel : BaseCreateViewModel<JobPartService, JobPartDto,  JobPart>
    {
        public ICommand SelectPartCommand { get; set; }
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
        public int PartId
        {
            get => Model.PartId;
            set
            {
                if (Model.PartId != value)
                {
                    Model.PartId = value;
                    OnPropertyChanged(() => PartId);
                    //whenewer part id changes we download default price from unit price
                    Cost = Service.GetPartCostByPartId(value);
                }
            }
        }
        public int QuantityUsed
        {
            get => Model.QuantityUsed;
            set
            {
                if (Model.QuantityUsed != value)
                {
                    Model.QuantityUsed = value;
                    OnPropertyChanged(() => QuantityUsed);
                }
            }
        }
        public decimal? Cost
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
        private string? _PartName { get; set; }
        public string? PartName
        {
            get => _PartName;
            set
            {
                if (_PartName != value)
                {
                    _PartName = value;
                    OnPropertyChanged(() => PartName);
                }
            }
        }
        private int _NumberOfActiveParts;
        public int NumberOfActiveParts
        {
            get => _NumberOfActiveParts;
            set
            {
                if (_NumberOfActiveParts != value)
                {
                    _NumberOfActiveParts = value;
                    OnPropertyChanged(() => NumberOfActiveParts);
                }
            }
        }
        public JobPartViewModel() : base("Job Part")
        {
            PartName = "Select Part";
            SelectPartCommand = new BaseCommand(() => SelectPart());
            NumberOfActiveParts = Service.InitializeNumberOfActivePart();
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<int>>(this, (recipient, message) =>
            {
                if (message.WhoRequestedToSelect is AddNewRepairViewModel)
                {
                    JobId = message.SelectedObject;
                }
            });
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<PartDto>>(this, (recipient, message) => GetSelectedPart(message));
        }
        public JobPartViewModel(int id) : base(id, "Job Part")
        {
            PartName = "Select Part";
            SelectPartCommand = new BaseCommand(() => SelectPart());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<int>>(this, (recipient, message) =>
            {
                if (message.WhoRequestedToSelect is AddNewRepairViewModel)
                {
                    JobId = message.SelectedObject;
                }
            });
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<PartDto>>(this, (recipient, message) => GetSelectedPart(message));
        }
        public void SelectPart()
        {
            //sending message to main window view model to open view with parts
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new PartsWithCallbackViewModel(this)
            });

        }
        public void GetSelectedPart(SelectedObjectMessage<PartDto>  message)
        {
            if(message.WhoRequestedToSelect == this)
            {
                PartId = message.SelectedObject.Id;
                PartName = message.SelectedObject.PartName;
            }
        }
    }
}

