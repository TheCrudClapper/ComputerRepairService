using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;

namespace ComputerRepairService.ViewModels
{
    public class AddNewPositionViewModel : BaseCreateViewModel<RoleService, RoleDto, Role>
    {
        public string RoleName
        {
            get => Model.RoleName;
            set
            {
                if (Model.RoleName != value)
                {
                    Model.RoleName = value;
                    OnPropertyChanged(() => RoleName);
                }
            }
        }
        public string? RoleDescription
        {
            get => Model.RoleDescription;
            set
            {
                if (Model.RoleDescription != value)
                {
                    Model.RoleDescription = value;
                    OnPropertyChanged(() => RoleDescription);
                }
            }
        }
        public int _numberOfActiveRoles;
        public int NumberOfActiveRoles
        {
            get => _numberOfActiveRoles;
            set
            {
                if (_numberOfActiveRoles != value)
                {
                    _numberOfActiveRoles = value;
                    OnPropertyChanged(() => NumberOfActiveRoles);
                }
            }
        }
        public AddNewPositionViewModel() : base("Position")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveRoles = Service.InitializeNumberOfActiveRoles();
        }
        public AddNewPositionViewModel(int id) : base(id, "Position")
        {
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveRoles = Service.InitializeNumberOfActiveRoles();
        }
        public override void ClearInputFields()
        {
            RoleName = "";
            RoleDescription = "";
        }
    }
}
