using ComputerRepairService.Models.Dtos;
namespace ComputerRepairService.Models.Servicess
{
    public class ServiceService : BaseService<ServiceDto, Service>
    {
        public decimal? ServicePartCostFrom { get; set; }
        public decimal? ServicePartCostTo { get; set; }
        public override void AddOrUpdateModel(Service model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Services.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(ServiceDto model)
        {
            Service service = DatabaseContext.Services.First(item => item.Id == model.Id);
            service.IsActive = false;
            service.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Service GetModel(int id)
        {
            return DatabaseContext.Services.First(item => item.Id == id);
        }

        public override List<ServiceDto> GetModels()
        {
            IQueryable<Service> service = DatabaseContext.Services.Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Service.ServiceName):
                        service = service.Where(item => item.ServiceName.Contains(SearchInput));
                        break;
                    case nameof(Service.ServiceDescription):
                        service = service.Where(item => item.ServiceDescription.Contains(SearchInput));
                        break;
                }
            }
            if (ServicePartCostFrom != null)
            {
                service = service.Where(item => item.BasePrice >= ServicePartCostFrom);
            }
            if (ServicePartCostTo != null)
            {
                service = service.Where(item => item.BasePrice <= ServicePartCostTo);
            }

            IQueryable<ServiceDto> serviceDtos = service.Select(item => new ServiceDto()
            {
                Id = item.Id,
                BasePrice = item.BasePrice,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                ServiceDescription = item.ServiceDescription,
                ServiceName = item.ServiceName
            });
            switch (OrderProperty)
            {
                case nameof(Service.ServiceName):
                    serviceDtos = OrderAscending ? serviceDtos.OrderBy(item => item.ServiceName) : serviceDtos.OrderByDescending(item => item.ServiceName);
                    break;
                case nameof(Service.BasePrice):
                    serviceDtos = OrderAscending ? serviceDtos.OrderBy(item => item.BasePrice) : serviceDtos.OrderByDescending(item => item.BasePrice);
                    break;
                case nameof(Service.DateCreated):
                    serviceDtos = OrderAscending ? serviceDtos.OrderBy(item => item.DateCreated) : serviceDtos.OrderByDescending(item => item.DateCreated);
                    break;
            }
            return serviceDtos.ToList();
        }

        public override Service CreateModel()
        {
            return new Service()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(Service model)
        {
            DatabaseContext.Services.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int InitializeNumberOfActiveServices()
        {
            return DatabaseContext.Services.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto
                {
                    DisplayName = "Name",
                    PropertyTitle = nameof(Service.ServiceName)
                },
                new SearchComboBoxDto
                {
                    DisplayName = "Description",
                    PropertyTitle = nameof(Service.ServiceDescription)
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Service.ServiceName),
                    DisplayName = "Name",
                },
                 new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Service.BasePrice),
                    DisplayName = "Price",
                },
                  new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Service.DateCreated),
                    DisplayName = "Date Created",
                },
            };
        }
        public override string ValidateProperty(string columnName, Service model)
        {
            if (columnName == nameof(Service.ServiceName))
            {
                if (string.IsNullOrWhiteSpace(model.ServiceName))
                {
                    return "Service Name is required";
                }
                if (int.TryParse(model.ServiceName, out _))
                {
                    return "Service Name can't be number";
                }
            }
            if (columnName == nameof(Service.BasePrice))
            {
                //validating if base price string can be converted to decimal
                string basePriceString = model.BasePrice.ToString();
                if (!decimal.TryParse(basePriceString, out _))
                {
                    return "Service Price can't have letters !";
                }
                if (model.BasePrice <= 0)
                {
                    return "Service Price can't be 0 or negative";
                }
            }
            return string.Empty;
        }
    }
}
