using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace ComputerRepairService.Models.Servicess
{
    public class SupplierService : BaseService<SupplierDto, Supplier>
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public override void AddOrUpdateModel(Supplier model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Suppliers.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }
        public override void DeleteModel(SupplierDto model)
        {
            Supplier supplier = DatabaseContext.Suppliers.First(item => item.Id == model.Id);
            supplier.IsActive = false;
            supplier.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Supplier GetModel(int id)
        {
            return DatabaseContext.Suppliers.Include(item => item.Address).First(item => item.Id == id);
        }

        public override List<SupplierDto> GetModels()
        {
            IQueryable<Supplier> suppliers = DatabaseContext.Suppliers.Include(item => item.Address).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Supplier.Title):
                        suppliers = suppliers.Where(item => item.Title.Contains(SearchInput));
                        break;
                    case nameof(Supplier.PhoneNumber):
                        suppliers = suppliers.Where(item => item.PhoneNumber.Contains(SearchInput));
                        break;
                    case nameof(Supplier.Address.Place):
                        suppliers = suppliers.Where(item => item.Address.Place.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null)
            {
                suppliers = suppliers.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                suppliers = suppliers.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<SupplierDto> suppliersDto = suppliers.Select(item => new SupplierDto()
            {
                Id = item.Id,
                Nip = item.Nip,
                Title = item.Title,
                Street = item.Address.Street,
                Place = item.Address.Place,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                HouseNumber = item.Address.HouseNumber,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
            });
            switch (OrderProperty)
            {
                case nameof(SupplierDto.Title):
                    suppliersDto = OrderAscending ? suppliersDto.OrderBy(item => item.Title) : suppliersDto.OrderByDescending(item => item.Title);
                    break;
                case nameof(SupplierDto.Place):
                    suppliersDto = OrderAscending ? suppliersDto.OrderBy(item => item.Place) : suppliersDto.OrderByDescending(item => item.Place);
                    break;
                case nameof(SupplierDto.Email):
                    suppliersDto = OrderAscending ? suppliersDto.OrderBy(item => item.Email) : suppliersDto.OrderByDescending(item => item.Email);
                    break;
                case nameof(SupplierDto.Nip):
                    suppliersDto = OrderAscending ? suppliersDto.OrderBy(item => item.Nip) : suppliersDto.OrderByDescending(item => item.Nip);
                    break;
            }
            return suppliersDto.ToList();
        }

        public override Supplier CreateModel()
        {
            return new Supplier()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
                Address = new()
                {
                    IsActive = true,
                    DateCreated = DateTime.Now,
                }
            };
        }

        public override void UpdateModel(Supplier model)
        {
            DatabaseContext.Suppliers.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int InitializeNumberOfActiveSuppliers()
        {
            return DatabaseContext.Suppliers.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    DisplayName = "Title",
                    PropertyTitle = nameof(Supplier.Title),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Phone Number",
                    PropertyTitle = nameof(Supplier.PhoneNumber),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Place",
                    PropertyTitle = nameof(Supplier.Address.Place),
                }

            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {

                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(SupplierDto.Title),
                    DisplayName = "Title"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(SupplierDto.Place),
                    DisplayName = "Place"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(SupplierDto.Email),
                    DisplayName = "Email"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(SupplierDto.Nip),
                    DisplayName = "Nip"
                },

            };
        }
        public override string ValidateProperty(string columnName, Supplier model)
        {
            if (columnName == nameof(Supplier.Title))
            {
                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    return "Title is required";
                }
            }
            else if (columnName == nameof(Supplier.PhoneNumber))
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
            else if (columnName == nameof(Supplier.Address.HouseNumber))
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
            else if (columnName == nameof(Supplier.Address.Country))
            {
                if (string.IsNullOrWhiteSpace(model.Address.Country))
                {
                    return "Country is required";
                }
            }
            else if (columnName == nameof(Supplier.Address.PostalCity))
            {
                if (string.IsNullOrWhiteSpace(model.Address.PostalCity))
                {
                    return "Postal City is required";
                }
            }
            else if (columnName == nameof(Supplier.Address.PostalCode))
            {
                if (string.IsNullOrWhiteSpace(model.Address.PostalCode))
                {
                    return "Postal Code is required";
                }

            }
            else if (columnName == nameof(Supplier.Email))
            {
                if (model.Email != null)
                {
                    if (!model.Email.Contains("@"))
                    {
                        return "Please, provide valid email";
                    }
                }
            }
            return string.Empty;
        }
    }
}

