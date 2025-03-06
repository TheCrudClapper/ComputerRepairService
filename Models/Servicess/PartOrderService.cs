using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ComputerRepairService.Models.Servicess
{
    public class PartOrderService : BaseService<PartOrderDto, PartOrder>
    {
        public decimal? UnitPriceFrom { get; set; }
        public decimal? UnitPriceTo { get; set; }
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public bool HasDeliveryDate { get; set; }
        public override void AddOrUpdateModel(PartOrder model)
        {
            if (model.Id == default)
            {
                DatabaseContext.PartOrders.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(PartOrderDto model)
        {
            PartOrder partOrder = DatabaseContext.PartOrders.First(item => item.Id == model.Id);
            partOrder.IsActive = false;
            partOrder.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override PartOrder GetModel(int id)
        {
            return DatabaseContext.PartOrders.First(item => item.Id == id);
        }

        public override List<PartOrderDto> GetModels()
        {
            IQueryable<PartOrder> partOrders = DatabaseContext.PartOrders.Include(item => item.Supplier).Include(item => item.Part).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(PartOrder.Supplier.Title):
                        partOrders = partOrders.Where(item => item.Supplier.Title.Contains(SearchInput));
                        break;
                    case nameof(PartOrder.Part.PartName):
                        partOrders = partOrders.Where(item => item.Part.PartName.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null)
            {
                partOrders = partOrders.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                partOrders = partOrders.Where(item => item.DateCreated <= DateCreatedTo);
            }
            if (UnitPriceFrom != null)
            {
                partOrders = partOrders.Where(item => item.Part.UnitPrice >= UnitPriceFrom);
            }
            if (UnitPriceTo != null)
            {
                partOrders = partOrders.Where(item => item.Part.UnitPrice <= UnitPriceTo);
            }
            if (HasDeliveryDate)
            {
                partOrders = partOrders.Where(item => item.DeliveryDate != null);
            }
            IQueryable<PartOrderDto> partOrderDtos = partOrders.Select(item => new PartOrderDto()
            {
                Id = item.Id,
                QuantityOrdered = item.QuantityOrdered,
                TotalCost = item.Part.UnitPrice * item.QuantityOrdered,
                PartName = item.Part.PartName,
                Title = item.Supplier.Title,
                UnitPrice = item.Part.UnitPrice,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                DeliveryDate = item.DeliveryDate,
                OrderDate = item.OrderDate,

            });
            switch (OrderProperty)
            {
                case nameof(PartOrderDto.Title):
                    partOrderDtos = OrderAscending ? partOrderDtos.OrderBy(item => item.Title) : partOrderDtos.OrderByDescending(item => item.Title);
                    break;
                case nameof(PartOrderDto.TotalCost):
                    partOrderDtos = OrderAscending ? partOrderDtos.OrderBy(item => item.TotalCost) : partOrderDtos.OrderByDescending(item => item.TotalCost);
                    break;
                case nameof(PartOrderDto.PartName):
                    partOrderDtos = OrderAscending ? partOrderDtos.OrderBy(item => item.PartName) : partOrderDtos.OrderByDescending(item => item.PartName);
                    break;
                case nameof(PartOrderDto.QuantityOrdered):
                    partOrderDtos = OrderAscending ? partOrderDtos.OrderBy(item => item.QuantityOrdered) : partOrderDtos.OrderByDescending(item => item.QuantityOrdered);
                    break;
            }
            return partOrderDtos.ToList();
        }

        public override PartOrder CreateModel()
        {
            return new PartOrder()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(PartOrder model)
        {
            DatabaseContext.PartOrders.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int InitializeNumberOfActiveOrder()
        {
            return DatabaseContext.PartOrders.Where(item => item.IsActive).Count();
        }
        public ObservableCollection<ComboBoxDto> InitializePartsComboBox()
        {

            IQueryable<ComboBoxDto> comboBoxDto = DatabaseContext.Parts.Where(item => item.IsActive).Select(item => new ComboBoxDto()
            {
                Id = item.Id,
                Title = item.PartName,
            });
            return new ObservableCollection<ComboBoxDto>(comboBoxDto.ToList());
        }
        public ObservableCollection<ComboBoxDto> InitializeSuppliersComboBox()
        {
            IQueryable<ComboBoxDto> comboBoxDto = DatabaseContext.Suppliers.Where(item => item.IsActive).Select(item => new ComboBoxDto()
            {
                Id = item.Id,
                Title = item.Title,
            });
            return new ObservableCollection<ComboBoxDto>(comboBoxDto.ToList());
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartOrder.Supplier.Title),
                    DisplayName = "Supplier",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartOrder.Part.PartName),
                    DisplayName = "Part",
                },
            };
        }
        public decimal GetTotalOrderWorthByPartId(int partId, int quantity)
        {
            return DatabaseContext.Parts.Where(item => item.Id == partId).Select(item => item.UnitPrice * quantity).First();
        }
        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    DisplayName = "Supplier",
                    PropertyTitle = nameof(PartOrderDto.Title),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Total Cost",
                    PropertyTitle = nameof(PartOrderDto.TotalCost),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Part",
                    PropertyTitle = nameof(PartOrderDto.PartName),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Quantity Ordered",
                    PropertyTitle = nameof(PartOrderDto.QuantityOrdered),
                },
            };
        }
        public override string ValidateProperty(string columnName, PartOrder model)
        {
            if (columnName == nameof(PartOrder.QuantityOrdered))
            {
                string quantityAsString = model.QuantityOrdered.ToString();
                if (!int.TryParse(quantityAsString, out _))
                {
                    return "Quantity can't be chars";
                }
                if (model.QuantityOrdered == default(int) || model.QuantityOrdered < 0)
                {
                    return "Quantity can't be 0 or lower";
                }
            }
            if (columnName == nameof(PartOrder.SupplierId))
            {
                if (model.SupplierId == default(int))
                {
                    return "Supplier is required !";
                }
            }
            if (columnName == nameof(PartOrder.PartId))
            {
                if (model.PartId == default(int))
                {
                    return "Part is required !";
                }
            }
            if (columnName == nameof(PartOrder.OrderDate))
            {
                if (model.OrderDate < DateTime.Today)
                {
                    return "Cannot set date in past !";
                }
            }
            if (columnName == nameof(PartOrder.DeliveryDate))
            {
                if (model.DeliveryDate < DateOnly.FromDateTime(DateTime.Today))
                {
                    return "Cannot set date in past";
                }
            }
            return string.Empty;
        }
    }
}
