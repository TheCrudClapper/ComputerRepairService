using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.ViewModels.Single
{
    public class JobStatusViewModel : BaseCreateViewModel<JobStatusesService, JobStatusDto, JobStatus>
    {
        public string StatusName
        {
            get => Model.StatusName;
            set
            {
                if(Model.StatusName != value)
                {
                    Model.StatusName = value;
                    OnPropertyChanged(() => StatusName);
                }
            }
        }
        public string? StatusDescription
        {
            get => Model.StatusDescription;
            set
            {
                if (Model.StatusDescription != value)
                {
                    Model.StatusDescription = value;
                    OnPropertyChanged(() => StatusDescription);
                }
            }
        }
        private int _ActiveJobStatuses { get; set; }
        public int ActiveJobStatuses
        {
            get => _ActiveJobStatuses;
            set
            {
                if (_ActiveJobStatuses != value)
                {
                    _ActiveJobStatuses = value;
                    OnPropertyChanged(() => ActiveJobStatuses);
                }
            }
        }
        public JobStatusViewModel() : base("Job Status")
        {
            ActiveJobStatuses = Service.GetNumberOfActiveStatuses();
        }
        public JobStatusViewModel(int id) : base(id,"Job Status")
        {
            ActiveJobStatuses = Service.GetNumberOfActiveStatuses();
        }
    }
}
