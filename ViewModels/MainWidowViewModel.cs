using CommunityToolkit.Mvvm.Messaging;
using ComputerRepairService.Helpers;
using ComputerRepairService.ViewModels.Business;
using ComputerRepairService.ViewModels.Many;
using ComputerRepairService.ViewModels.Single;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels
{
    public class MainWidowViewModel : BaseViewModel
    {
        public ICommand OpenAddCustomerView { get => new BaseCommand(() => CreateView(new AddCustomerViewModel())); }
        public ICommand OpenCustomerManagmentView { get => new BaseCommand(() => CreateView(new CustomersViewModel())); }
        public ICommand OpenAddEmployeeView { get => new BaseCommand(() => CreateView(new AddEmployeeViewModel())); }
        public ICommand OpenManageEmployeeView { get => new BaseCommand(() => CreateView(new EmployeesViewModel())); }
        public ICommand OpenAddNewPositionView { get => new BaseCommand(() => CreateView(new AddNewPositionViewModel())); }
        public ICommand OpenManagePositionView { get => new BaseCommand(() => CreateView(new PositionsViewModel())); }
        public ICommand OpenAddPartCategoriesView { get => new BaseCommand(() => CreateView(new AddPartCategoryViewModel())); }
        public ICommand OpenManagePartCategoriesView { get => new BaseCommand(() => CreateView(new PartCategoriesViewModel())); }
        public ICommand OpenManageSuppliersView { get => new BaseCommand(() => CreateView(new SuppliersViewModel())); }
        public ICommand OpenAddSuppliersView { get => new BaseCommand(() => CreateView(new AddSupplierViewModel())); }
        public ICommand OpenAddServicesView { get => new BaseCommand(() => CreateView(new AddServiceViewModel())); }
        public ICommand OpenManageServicesView { get => new BaseCommand(() => CreateView(new ServicesViewModel())); }
        public ICommand OpenAddPartView { get => new BaseCommand(() => CreateView(new AddPartViewModel())); }
        public ICommand OpenManageParts { get => new BaseCommand(() => CreateView(new PartsViewModel())); }
        public ICommand OpenManageInvoices { get => new BaseCommand(() => CreateView(new InvoicesViewModel())); }
        public ICommand OpenManageFeedbacks { get => new BaseCommand(() => CreateView(new FeedbacksViewModel())); }
        public ICommand OpenAddInvoiceView { get => new BaseCommand(() => CreateView(new AddInvoiceViewModel())); }
        public ICommand OpenAddOrderView { get => new BaseCommand(() => CreateView(new AddOrderViewModel())); }
        public ICommand OpenManageOrdersView { get => new BaseCommand(() => CreateView(new OrdersViewModels())); }
        public ICommand OpenManageRepairsView { get => new BaseCommand(() => CreateView(new RepairsViewModel())); }
        public ICommand OpenAddNewRepairView { get => new BaseCommand(() => CreateView(new AddNewRepairViewModel())); }
        public ICommand OpenAddScheduleView { get => new BaseCommand(() => CreateView(new AddScheduleViewModel())); }
        public ICommand OpenManageSchedulesView { get => new BaseCommand(() => CreateView(new SchedulesViewModel())); }
        public ICommand OpenDashboardView { get => new BaseCommand(() => CreateView(new DashboardViewModel())); }
        public ICommand OpenRaportView { get => new BaseCommand(() => CreateView(new RaportViewModel())); }
        public ICommand OpenJobStatusesView { get => new BaseCommand(() => CreateView(new JobStatusesViewModel())); }
        public ICommand OpenJobStatus { get => new BaseCommand(() => CreateView(new JobStatusViewModel())); }
        public ICommand ExitAppCommand { get; set; }
        public MainWidowViewModel()
        {
            ExitAppCommand = new BaseCommand(() => ExitApp());
            Workspaces = new();
            Workspaces.CollectionChanged += OnWorkspacesChanged;
            OpenDashboardView.Execute("");
            WeakReferenceMessenger.Default.Register<OpenViewMessage>(this, (recipent, message) => OpenView(message));
        }


        public ObservableCollection<WorkspaceViewModel> Workspaces { get; set; }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= onWorkspaceRequestClose;
        }

        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel? workspace = sender as WorkspaceViewModel;
            if (workspace != null)
            {
                Workspaces.Remove(workspace);
            }
        }

        #region Helepers
        private void OpenView(OpenViewMessage message)
        {
            CreateView(message.ViewModelToBeOpened);
        }
        private void CreateView(WorkspaceViewModel workspace)
        {
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        private void CreateListView<WorkspaceViewModelType>() where WorkspaceViewModelType : WorkspaceViewModel, new()
        {
            WorkspaceViewModel? workspace = Workspaces.FirstOrDefault(vm => vm is WorkspaceViewModelType);
            if (workspace == null)
            {
                workspace = new WorkspaceViewModelType();
                Workspaces.Add(workspace);
            }
            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
