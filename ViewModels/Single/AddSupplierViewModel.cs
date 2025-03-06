using ComputerRepairService.Helpers;
using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
using System.Collections.ObjectModel;
namespace ComputerRepairService.ViewModels
{
    public class AddSupplierViewModel : BaseCreateViewModel<SupplierService, SupplierDto, Supplier>
    {
        public string Title
        {
            get => Model.Title;
            set
            {
                if (Model.Title != value)
                {
                    Model.Title = value;
                    OnPropertyChanged(() => Title);
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
        public string? Nip
        {
            get => Model.Nip;
            set
            {
                if (Model.Nip != value)
                {
                    Model.Nip = value;
                    OnPropertyChanged(() => Nip);
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
        public string? Place
        {
            get => Model.Address.Place;
            set
            {
                if (Model.Address.Place != value)
                {
                    Model.Address.Place = value;
                    OnPropertyChanged(() => Place);
                }
            }
        }
        public string? Street
        {
            get => Model.Address.Street;
            set
            {
                if (Model.Address.Street != value)
                {
                    Model.Address.Street = value;
                    OnPropertyChanged(() => Street);
                }
            }
        }
        public string? HouseNumber
        {
            get => Model.Address.HouseNumber;
            set
            {
                if (Model.Address.HouseNumber != value)
                {
                    Model.Address.HouseNumber = value;
                    OnPropertyChanged(() => HouseNumber);
                }
            }
        }
        public string PostalCity
        {
            get => Model.Address.PostalCity;
            set
            {
                if (Model.Address.PostalCity != value)
                {
                    Model.Address.PostalCity = value;
                    OnPropertyChanged(() => PostalCity);
                }
            }
        }
        public string PostalCode
        {
            get => Model.Address.PostalCode;
            set
            {
                if (Model.Address.PostalCode != value)
                {
                    Model.Address.PostalCode = value;
                    OnPropertyChanged(() => PostalCode);
                }
            }
        }
        public string Country
        {
            get => Model.Address.Country;
            set
            {
                if (Model.Address.Country != value)
                {
                    Model.Address.Country = value;
                    OnPropertyChanged(() => Country);
                }
            }
        }
        private int _numberOfActiveSuppliers;
        public int NumberOfActiveSuppliers
        {
            get => _numberOfActiveSuppliers;
            set
            {
                if (_numberOfActiveSuppliers != value)
                {
                    _numberOfActiveSuppliers = value;
                    OnPropertyChanged(() => NumberOfActiveSuppliers);
                }
            }
        }
        private ObservableCollection<string> _Countries { get; set; }
        public ObservableCollection<string> Countries
        {
            get => _Countries;
            set
            {
                if (_Countries != value)
                {
                    _Countries = value;
                    OnPropertyChanged(() => Countries);
                }
            }
        }

        public AddSupplierViewModel() : base("Supplier")
        {
            InitializeCountryCollection();
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveSuppliers = Service.InitializeNumberOfActiveSuppliers();
        }
        public AddSupplierViewModel(int id) : base(id, "Supplier")
        {
            InitializeCountryCollection();
            ClearInputsCommand = new BaseCommand(() => ClearInputFields());
            NumberOfActiveSuppliers = Service.InitializeNumberOfActiveSuppliers();
        }
        public override void ClearInputFields()
        {
            Title = string.Empty;
            Nip = string.Empty;
            Country = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Place = string.Empty;
            Street = string.Empty;
            HouseNumber = string.Empty;
            PostalCity = string.Empty;
            PostalCode = string.Empty;
        }
        public void InitializeCountryCollection()
        {
            _Countries = new ObservableCollection<string>()
            {
                "Poland",
                "Germany",
                "Slovakia",
                "Lativa",
                "Russia",
                "Bri'sh",
                "USA",
            };
        }
    }
}
