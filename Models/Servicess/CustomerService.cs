using ComputerRepairService.Helpers;
using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ComputerRepairService.Models.Servicess
{
    public class CustomerService : BaseService<CustomerDto, Customer>
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public bool HasNip { get; set; }
        public YesNoEnum HasPhoneNumber { get; set; }
        public CustomerService()
        {

        }
        public override void AddOrUpdateModel(Customer model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Customers.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(CustomerDto model)
        {
            Customer customer = DatabaseContext.Customers.First(item => item.Id == model.Id);
            customer.IsActive = false;
            customer.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Customer GetModel(int id)
        {
            return DatabaseContext.Customers.Include(item => item.Address).First(item => item.Id == id);
        }

        public override List<CustomerDto> GetModels()
        {
            IQueryable<Customer> customers = DatabaseContext.Customers.Include(item => item.Address).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Customer.FirstName):
                        customers = customers.Where(item => item.FirstName.Contains(SearchInput));
                        break;
                    case nameof(Customer.Nip):
                        customers = customers.Where(item => item.Nip.Contains(SearchInput));
                        break;
                    case nameof(Customer.Address.Place):
                        customers = customers.Where(item => item.Address.Place.Contains(SearchInput));
                        break;
                }
            }
            if (HasNip)
            {
                customers = customers.Where(item => !string.IsNullOrEmpty(item.Nip));
            }
            switch (HasPhoneNumber)
            {
                case YesNoEnum.Yes:
                    customers = customers.Where(item => !string.IsNullOrEmpty(item.PhoneNumber));
                    break;
                case YesNoEnum.No:
                    customers = customers.Where(item => string.IsNullOrEmpty(item.PhoneNumber));
                    break;
            }
            if (DateCreatedFrom != null)
            {
                customers = customers.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                customers = customers.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<CustomerDto> customerDtos = customers.Select(item => new CustomerDto()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                Email = item.Email,
                FirstName = item.FirstName,
                Title = item.Title,
                Surname = item.Surname,
                Nip = item.Nip,
                PhoneNumber = item.PhoneNumber,
                Place = item.Address.Place,
                Street = item.Address.Street,
                HouseNumer = item.Address.HouseNumber,
            });
            switch (OrderProperty)
            {
                case nameof(Customer.FirstName):
                    customerDtos = OrderAscending ? customerDtos.OrderBy(item => item.FirstName) : customerDtos.OrderByDescending(item => item.FirstName);
                    break;
                case nameof(Customer.Surname):
                    customerDtos = OrderAscending ? customerDtos.OrderBy(item => item.Surname) : customerDtos.OrderByDescending(item => item.Surname);
                    break;
                case nameof(Customer.Address.Place):
                    customerDtos = OrderAscending ? customerDtos.OrderBy(item => item.Place) : customerDtos.OrderByDescending(item => item.Place);
                    break;
            }
            return customerDtos.ToList();
        }
        public override Customer CreateModel()
        {
            return new Customer()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
                Address = new()
                {
                    IsActive = true,
                    DateCreated = DateTime.Now,
                },
            };
        }
        public override void UpdateModel(Customer model)
        {
            model.DateEdited = DateTime.Now;
            DatabaseContext.Customers.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int InitializeNumberOfActiveCustomers()
        {
            return DatabaseContext.Customers.Where(item => item.IsActive).Count();
        }
        public ObservableCollection<string> InitializeCountriesCollection()
        {
            return new ObservableCollection<string>()
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

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.FirstName),
                    DisplayName = "First Name",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Nip),
                    DisplayName = "Nip"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Address.Place),
                    DisplayName = "Place"
                }
            };

        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.FirstName),
                    DisplayName = "Name",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Surname),
                    DisplayName = "Surname"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Customer.Address.Place),
                    DisplayName = "Place"
                }
            };
        }
        public override string ValidateProperty(string columnName, Customer model)
        {
            if (columnName == nameof(Customer.FirstName))
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                {
                    return "First Name is required";
                }
            }
            else if (columnName == nameof(Customer.Surname))
            {
                if (string.IsNullOrWhiteSpace(model.Surname))
                {
                    return "First Name is required";
                }
            }
            else if (columnName == nameof(Customer.Email))
            {
                if (model.Email != null)
                {
                    if (!model.Email.Contains("@"))
                    {
                        return "Please, provide valid email";
                    }
                }
            }
            else if (columnName == nameof(Customer.Address.PostalCity))
            {
                if (string.IsNullOrWhiteSpace(model.Address.PostalCity))
                {
                    return "Postal City is required";
                }
                if (int.TryParse(model.Address.PostalCity, out _))
                {
                    return "Postal City cannot be number";
                }

            }
            else if (columnName == nameof(Customer.Address.PostalCode))
            {
                if (string.IsNullOrWhiteSpace(model.Address.PostalCode))
                {
                    return "Postal Code is required";
                }

            }
            else if (columnName == nameof(Customer.Address.Country))
            {
                if (string.IsNullOrWhiteSpace(model.Address.Country))
                {
                    return "Country is required";
                }
            }
            else if (columnName == nameof(Customer.PhoneNumber))
            {
                if (model.PhoneNumber != null)
                {
                    if (!int.TryParse(model.PhoneNumber, out _))
                    {
                        return "Use only numbers";
                    }
                    if (model.PhoneNumber.Length < 9)
                    {
                        return "Too less numbers, correct it";
                    }
                }
            }
            else if (columnName == nameof(Customer.Address.HouseNumber))
            {
                if (string.IsNullOrWhiteSpace(model.Address.HouseNumber))
                {
                    return "House Number is required";
                }
                if (!int.TryParse(model.Address.HouseNumber, out _))
                {
                    return "House number must be an number";
                }
            }
            return string.Empty;
        }
    }
}
