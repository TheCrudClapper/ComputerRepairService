using ComputerRepairService.ViewModels;

namespace ComputerRepairService.Helpers
{
    public class OpenViewMessage
    {
        //view model supposed to be open
        public WorkspaceViewModel ViewModelToBeOpened { get; set; } = null!;
        //who send this message
        public object Sender { get; set; } = default!;

    }
}
