using ComputerRepairService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Helpers
{
    public class OpenViewMessage
    {
        //view model supposed to be open
        public WorkspaceViewModel ViewModelToBeOpened { get; set; } = null!;
        //who send this message
        public object Sender {  get; set; } = default!;

    }
}
