using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.ViewModels.Many
{
    public class SchedulesViewModel : BaseManyViewModel<ScheduleService, ScheduleDto, Schedule>
    {
        public bool HasEndDate
        {
            get => Service.HasEndDate;
            set
            {
                if (Service.HasEndDate != value)
                {
                    Service.HasEndDate = value;
                    OnPropertyChanged(() => HasEndDate);
                }
            }
        }
        public DateTime? DateDue
        {
            get => Service.DateDue;
            set
            {
                if (Service.DateDue != value)
                {
                    Service.DateDue = value;
                    OnPropertyChanged(() => DateDue);
                }
            }
        }
        public DateTime? StartDate
        {
            get => Service.StartDate;
            set
            {
                if (Service.StartDate != value)
                {
                    Service.StartDate = value;
                    OnPropertyChanged(() => StartDate);
                }
            }
        }
        public DateTime? EndDate
        {
            get => Service.EndDate;
            set
            {
                if (Service.EndDate != value)
                {
                    Service.EndDate = value;
                    OnPropertyChanged(() => EndDate);
                }
            }
        }
        public SchedulesViewModel() : base("Schedules")
        {
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
        }

        protected override void ClearFilters()
        {
            HasEndDate = false;
            StartDate = null;
            EndDate = null;
            DateDue = null;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }
        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddScheduleViewModel(SelectedModel.Id)
                });
            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddScheduleViewModel()
            });
        }
    }
}
