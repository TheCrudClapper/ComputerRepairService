using ComputerRepairService.Helpers;
using ComputerRepairService.Models.BusinessObjects;
using ComputerRepairService.Models.Servicess;
using System.Windows.Input;

namespace ComputerRepairService.ViewModels.Business
{
    public class RaportViewModel : WorkspaceViewModel
    {
        //creating service

        private RaportService _raportService { get; set; } = null!;
        public ICommand GenerateCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public string IsVisible { get; set; }
        //creating mvvm properties
        private RaportDto _Raport { get; set; }
        public RaportDto Raport
        {
            get => _Raport;
            set
            {
                if (_Raport != value)
                {
                    _Raport = value;
                    OnPropertyChanged(() => Raport);
                }
            }
        }

        public DateTime? DateFrom
        {
            get => _raportService.DateFrom;
            set
            {
                if (_raportService.DateFrom != value)
                {
                    _raportService.DateFrom = value;
                    OnPropertyChanged(() => DateFrom);
                }
            }

        }
        public DateTime? DateTo
        {
            get => _raportService.DateTo;
            set
            {
                if (_raportService.DateTo != value)
                {
                    _raportService.DateTo = value;
                    OnPropertyChanged(() => DateTo);
                }
            }
        }
        public bool IsAll
        {
            get => _raportService.IsAll;
            set
            {
                if (_raportService.IsAll != value)
                {
                    _raportService.IsAll = value;
                    OnPropertyChanged(() => IsAll);
                }
            }
        }
        public RaportViewModel() : base("Raport")
        {
            //assigning service
            IsVisible = "Hidden";
            _raportService = new RaportService();
            IsAll = false;
            GenerateCommand = new BaseCommand(() => GenerateResult());
            ClearCommand = new BaseCommand(() => ClearFields());
        }
        //method for downloading data from service
        public void GenerateResult()
        {
            Raport = _raportService.GetRaportDto();
        }
        public void ClearFields()
        {
            IsAll = false;
            DateFrom = null;
            DateTo = null;
            Raport = null!;
        }
    }
}
