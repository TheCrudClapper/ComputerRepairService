using ComputerRepairService.Models.Servicess;

namespace ComputerRepairService.ViewModels
{
    public class BaseServiceViewModel<ServiceType, DtoType, ModelType>
    : WorkspaceViewModel where ServiceType : BaseService<DtoType, ModelType>, new()
    where ModelType : new()
    {
        public ServiceType Service { get; set; }
        public BaseServiceViewModel(string displayName) : base(displayName)
        {
            Service = new ServiceType();
        }
    }
}
