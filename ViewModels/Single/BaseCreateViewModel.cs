using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Servicess;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels.Single
{
    public class BaseCreateViewModel<ServiceType, DtoType, ModelType>
        : BaseServiceViewModel<ServiceType, DtoType, ModelType>, IDataErrorInfo
        where ServiceType : BaseService<DtoType, ModelType>, new()
        where DtoType : class
        where ModelType : class, new()
    {
        private ModelType _Model;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public ModelType Model
        {
            get => _Model;
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnPropertyChanged(() => Model);
                }
            }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ClearInputsCommand { get; set; }

        //default value for no error is empty string
        public string Error => string.Empty;
        //this is indexer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">Name of control that we are validating (name in binding)</param>
        /// <returns>Returns String.Empty if no errors or the error</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string this[string columnName]
        {
            get => Service.ValidateProperty(columnName, Model);
        }
        public BaseCreateViewModel(string displayName) : base(displayName)
        {
            _Model = Service.CreateModel();
            SaveCommand = new BaseCommand(() => Save());
            CancelCommand = new BaseCommand(() => Cancel());
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
        }
        public BaseCreateViewModel(int id, string displayName) : base(displayName)
        {
            _Model = Service.GetModel(id);
            SaveCommand = new BaseCommand(() => Save());
            CancelCommand = new BaseCommand(() => Cancel());
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
        }
        public virtual void Save()
        {
            try
            {
                if (Service.isValid(Model))
                {
                    Service.AddOrUpdateModel(Model);
                    OnRequestClose();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on save", "Error");
            }
        }
        public void Cancel()
        {
            OnRequestClose();
        }
        public virtual void ClearInputFields() { }

    }
}
