using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels.Single
{
    public class AddServiceViewModel : BaseCreateViewModel<ServiceService, ServiceDto, Service>
    {
        public string ServiceName
        {
            get => Model.ServiceName;
            set
            {
                if (Model.ServiceName != value)
                {
                    Model.ServiceName = value;
                    OnPropertyChanged(() => ServiceName);
                }
            }
        }
        public decimal BasePrice
        {
            get => Model.BasePrice;
            set
            {
                if (Model.BasePrice != value)
                {
                    Model.BasePrice = value;
                    OnPropertyChanged(() => BasePrice);
                }
            }
        }
        public string? ServiceDescription
        {
            get => Model.ServiceDescription;
            set
            {
                if (Model.ServiceDescription != value)
                {
                    Model.ServiceDescription = value;
                    OnPropertyChanged(() => ServiceDescription);
                }
            }
        }
        private int _numberOfActiveServices;
        public int NumberOfActiveServices
        {
            get => _numberOfActiveServices;
            set
            {
                if (_numberOfActiveServices != value)
                {
                    _numberOfActiveServices = value;
                    OnPropertyChanged(() => NumberOfActiveServices);
                }
            }
        }

        public override void ClearInputFields()
        {
            ServiceName = string.Empty;
            ServiceDescription = string.Empty;
            BasePrice = 0;
        }
        public AddServiceViewModel() : base("New Service")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveServices = Service.InitializeNumberOfActiveServices();
        }
        public AddServiceViewModel(int id) : base(id,"Service")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveServices = Service.InitializeNumberOfActiveServices();
        }
    }
}
