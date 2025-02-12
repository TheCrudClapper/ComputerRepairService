using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using ComputerRepairService.ViewModels.Single;
using ComputerRepairService.Models;
using ComputerRepairService.ViewResources;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ComputerRepairService.Helpers;
using System.Windows;

namespace ComputerRepairService.ViewModels
{
    public class AddCustomerViewModel : BaseCreateViewModel<CustomerService, CustomerDto, Customer>
    {
        
        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (Model.FirstName != value)
                {
                    Model.FirstName = value;
                    OnPropertyChanged(() => FirstName);
                }
            }
        }
        public string Surname
        {
            get => Model.Surname;
            set
            {
                if (Model.Surname != value)
                {
                    Model.Surname = value;
                    OnPropertyChanged(() => Surname);
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
        public string? Title
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
        private int _numberOfActiveCustomers;
        public int NumberOfActiveCustomers
        {
            get => _numberOfActiveCustomers;
            set
            {
                if(_numberOfActiveCustomers != value)
                {
                    _numberOfActiveCustomers = value;
                    OnPropertyChanged(() => NumberOfActiveCustomers);
                }
            }
        }
        private ObservableCollection<string> _Countries {  get; set; }
        public ObservableCollection<string> Countries
        {
            get => _Countries;
            set
            {
                if(_Countries != value)
                {
                    _Countries = value;
                    OnPropertyChanged(() => Countries);
                }
            }
        }
        public AddCustomerViewModel() : base("Customer")
        {
            Countries = Service.InitializeCountriesCollection();
            NumberOfActiveCustomers = Service.InitializeNumberOfActiveCustomers();
        }
        public AddCustomerViewModel(int id) : base(id, "Customer")
        {
            Countries = Service.InitializeCountriesCollection();
            NumberOfActiveCustomers = Service.InitializeNumberOfActiveCustomers();
        }
        public override void ClearInputFields()
        {
            FirstName = string.Empty;
            Surname = string.Empty;
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
    }
}
