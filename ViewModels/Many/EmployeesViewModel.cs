using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using ComputerRepairService.Models.Servicess;
using CommunityToolkit.Mvvm.Messaging;

namespace ComputerRepairService.ViewModels.Many
{
    public class EmployeesViewModel : BaseManyViewModel<EmployeeService, EmployeeDto, Employee>
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
        public bool HasRole
        {
            get => Service.HasRole;
            set
            {
                if (Service.HasRole != value)
                {
                    Service.HasRole = value;
                    OnPropertyChanged(() => HasRole);
                }
            }
        }
        public EmployeesViewModel() : base("Employees")
        {

        }

        protected override void ClearFilters()
        {
            DateCreatedFrom = null;
            DateCreatedTo = null;
            HasRole = false;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            Refresh();
        }

        protected override void HandleSelect()
        {
            if(SelectedModel != null)
            {
                WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
                {
                    Sender = this,
                    ViewModelToBeOpened = new AddEmployeeViewModel(SelectedModel.Id)
                });
            }
        }
        protected override void CreateNew()
        {
            WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
            {
                Sender = this,
                ViewModelToBeOpened = new AddEmployeeViewModel()
            });
        }
    }
}
