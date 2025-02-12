using ComputerRepairService.Models.Servicess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
