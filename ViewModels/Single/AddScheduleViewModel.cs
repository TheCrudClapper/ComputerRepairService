using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Many;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels.Single
{
    public class AddScheduleViewModel : BaseCreateViewModel<ScheduleService, ScheduleDto, Schedule>
    {
        public ICommand SelectRepairJobCommand { get; set; }
        public int EmployeeId
        {
            get => Model.EmployeeId;
            set
            {
                if (Model.EmployeeId != value)
                {
                    Model.EmployeeId = value;
                    OnPropertyChanged(() => EmployeeId);
                }
            }
        }
        public DateTime DateAssigned
        {
            get => Model.DateAssigned;
            set
            {
                if (Model.DateAssigned != value)
                {
                    Model.DateAssigned = value;
                    OnPropertyChanged(() => DateAssigned);
                }
            }
        }
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
        public DateTime StartDate
        {
            get => Model.StartDate;
            set
            {
                if (Model.StartDate != value)
                {
                    Model.StartDate = value;
                    OnPropertyChanged(() => StartDate);
                }
            }
        }
        public DateTime? EndDate
        {
            get => Model.StartDate;
            set
            {
                if (Model.EndDate != value)
                {
                    Model.EndDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }
        private ObservableCollection<ComboBoxDto> _Employees {  get; set; }
        public ObservableCollection<ComboBoxDto> Employees
        {
            get => _Employees;
            set
            {
                if(_Employees != value)
                {
                    _Employees = value;
                    OnPropertyChanged(() => Employees);
                }
            }
        }
        public int _NumberOfActiveSchedules { get; set; }
        public int NumberOfActiveSchedules
        {
            get => _NumberOfActiveSchedules;
            set
            {
                if (_NumberOfActiveSchedules != value)
                {
                    _NumberOfActiveSchedules = value;
                    OnPropertyChanged(() => NumberOfActiveSchedules);
                }
            }
        }
        public AddScheduleViewModel() : base("Schedule")
        {
            DateAssigned = DateTime.Now;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            NumberOfActiveSchedules = Service.InitializeNumberOfActiveSchedules();
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            Employees = Service.InitializeEmployeesComboBox();
            SelectRepairJobCommand = new BaseCommand(() => SelectRepairJob());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<RepairJobDto>>(this,(recipient, message) => GetSelectedRepairJob(message));
        }
        public AddScheduleViewModel(int id) : base(id, "Schedule")
        {
            NumberOfActiveSchedules = Service.InitializeNumberOfActiveSchedules();
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            Employees = Service.InitializeEmployeesComboBox();
            SelectRepairJobCommand = new BaseCommand(() => SelectRepairJob());
            WeakReferenceMessenger.Default.Register<SelectedObjectMessage<RepairJobDto>>(this, (recipient, message) => GetSelectedRepairJob(message));
        }
        private void SelectRepairJob()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new RepairsWithCallbackViewModel(this)
            });
        }
        private void GetSelectedRepairJob(SelectedObjectMessage<RepairJobDto> message)
        {
            if(message.WhoRequestedToSelect == this)
            {
                JobId = message.SelectedObject.Id;
            }
        }
        public override void ClearInputFields()
        {
            EmployeeId = 0;
            DateAssigned = DateTime.Now;
            EndDate = DateTime.Now;
            StartDate = DateTime.Now;
        }
    }
}
