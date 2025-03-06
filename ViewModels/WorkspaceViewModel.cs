using ComputerRepairService.Helpers;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels
{
    // Each tab view model will extends this class
    public class WorkspaceViewModel : BaseViewModel
    {
        #region FieldsAndProperties
        //each tab has display name and close command
        public string DisplayName { get; set; } //this name of the tab
        private BaseCommand _CloseCommand; //this is command to close a tab

        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => this.OnRequestClose()); //this command calls method to close a tab
                return _CloseCommand;
            }
        }
        public WorkspaceViewModel(string displayName)
        {
            DisplayName = displayName;
        }
        #endregion

        #region RequestClose [event]
        public event EventHandler RequestClose;
        public void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion 

    }
}
