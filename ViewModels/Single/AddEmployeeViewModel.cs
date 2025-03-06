using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
using System.Collections.ObjectModel;
namespace ComputerRepairService.ViewModels
{
    public class AddEmployeeViewModel : BaseCreateViewModel<EmployeeService, EmployeeDto, Employee>
    {
        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (Model.FirstName != value)
                {
                    Model.FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }
        public string Surname
        {
            get => Model.Surname;
            set
            {
                if (Model.Surname != value)
                {
                    Model.Surname = value;
                    OnPropertyChanged(() => Surname);
                }
            }
        }
        public int? RoleId
        {
            get => Model.RoleId;
            set
            {
                if (Model.RoleId != value)
                {
                    Model.RoleId = value;
                    OnPropertyChanged(() => RoleId);
                }
            }
        }
        private ObservableCollection<ComboBoxDto> _Roles { get; set; }
        public ObservableCollection<ComboBoxDto> Roles
        {
            get => _Roles;
            set
            {
                if (_Roles != value)
                {
                    _Roles = value;
                    OnPropertyChanged(() => Roles);
                }
            }
        }
        public string? PhoneNumber
        {
            get => Model.PhoneNumber;
            set
            {
                if (Model.PhoneNumber != value)
                {
                    Model.PhoneNumber = value;
                    OnPropertyChanged(() => PhoneNumber);
                }
            }
        }
        public string? Email
        {
            get => Model.Email;
            set
            {
                if (Model.Email != value)
                {
                    Model.Email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }
        public DateOnly HireDate
        {
            get => Model.HireDate;
            set
            {
                if (Model.HireDate != value)
                {
                    Model.HireDate = value;
                    OnPropertyChanged(() => HireDate);
                }
            }
        }
        public decimal Salary
        {
            get => Model.Salary;
            set
            {
                if (Model.Salary != value)
                {
                    Model.Salary = value;
                    OnPropertyChanged(() => Salary);
                }
            }
        }
        private int _NumberOfActiveEmployees { get; set; }
        public int NumberOfActiveEmployees
        {
            get => _NumberOfActiveEmployees;
            set
            {
                if (_NumberOfActiveEmployees != value)
                {
                    _NumberOfActiveEmployees = value;
                    OnPropertyChanged(() => NumberOfActiveEmployees);
                }
            }
        }
        public AddEmployeeViewModel() : base("New Employee")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            Roles = Service.InitializeRolesComboBox();
            NumberOfActiveEmployees = Service.InitializeNumberOfActiveEmployees();
        }
        public AddEmployeeViewModel(int id) : base(id, "New Employee")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            Roles = Service.InitializeRolesComboBox();
            NumberOfActiveEmployees = Service.InitializeNumberOfActiveEmployees();
        }
        public override void ClearInputFields()
        {
            FirstName = string.Empty;
            Surname = string.Empty;
            PhoneNumber = string.Empty;
            Salary = 0;
            Email = string.Empty;
            RoleId = default;
            HireDate = default;
        }

    }
}
