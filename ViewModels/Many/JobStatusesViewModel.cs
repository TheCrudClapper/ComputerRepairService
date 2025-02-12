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
using ComputerRepairService.ViewModels.Single;

namespace ComputerRepairService.ViewModels.Many
{
    public class JobStatusesViewModel : BaseManyViewModel<JobStatusesService, JobStatusDto, JobStatus>
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
        
        public JobStatusesViewModel() : base("Job Statuses")
        {
            
        }

        protected override void ClearFilters()
        {
           DateCreatedFrom = null;
           DateCreatedTo = null;
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
                ViewModelToBeOpened = new JobStatusViewModel()
            });
        }
        protected override void HandleSelect()
        {
            if (SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new JobStatusViewModel(SelectedModel.Id)
                });
            }
        }
    }
}
