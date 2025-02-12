using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComputerRepairService.Models.Servicess
{
    public class PartService : BaseService<PartDto, Part>
    {
        public decimal? PartPriceFrom { get; set; }
        public decimal? PartPriceTo { get; set; }
        public bool HasDescription { get; set; }
        public override void AddOrUpdateModel(Part model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Parts.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(PartDto model)
        {
            Part part = DatabaseContext.Parts.First(item => item.Id == model.Id);
            part.IsActive = false;
            part.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Part GetModel(int id)
        {
            return DatabaseContext.Parts.First(item => item.Id == id);
        }

        public override List<PartDto> GetModels()
        {
            IQueryable<Part> parts = DatabaseContext.Parts.Include(item => item.PartCategory).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Part.PartName):
                        parts = parts.Where(item => item.PartName.Contains(SearchInput));
                        break;
                    case nameof(Part.PartCategory.CategoryName):
                        parts = parts.Where(item => item.PartCategory.CategoryName.Contains(SearchInput));
                        break;
                    case nameof(Part.PartDescription):
                        parts = parts.Where(item => item.PartDescription.Contains(SearchInput));
                        break;
                }
            }
            if (HasDescription)
            {
                parts = parts.Where(item => !string.IsNullOrEmpty(item.PartDescription));
            }
            if (PartPriceFrom != null)
            {
                parts = parts.Where(item => item.UnitPrice >= PartPriceFrom);
            }
            if (PartPriceTo != null)
            {
                parts = parts.Where(item => item.UnitPrice <= PartPriceTo);
            }
            IQueryable<PartDto> partDtos = parts.Select(item => new PartDto()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                PartCategory = item.PartCategory.CategoryName,
                PartName = item.PartName,
                PartDescription = item.PartDescription,
                QuantityInStock = item.QuantityInStock,
                UnitPrice = item.UnitPrice,
            });
            switch (OrderProperty)
            {
                case nameof(Part.PartCategory):
                    partDtos = OrderAscending ? partDtos.OrderBy(item => item.PartCategory) : partDtos.OrderByDescending(item => item.PartCategory);
                    break;
                case nameof(Part.PartName):
                    partDtos = OrderAscending ? partDtos.OrderBy(item => item.PartName) : partDtos.OrderByDescending(item => item.PartName);
                    break;
                case nameof(Part.UnitPrice):
                    partDtos = OrderAscending ? partDtos.OrderBy(item => item.UnitPrice) : partDtos.OrderByDescending(item => item.UnitPrice);
                    break;
            }
            return partDtos.ToList();
        }

        public override Part CreateModel()
        {
            return new Part()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(Part model)
        {
            DatabaseContext.Parts.Update(model);
            DatabaseContext.SaveChanges();
        }
        public ObservableCollection<ComboBoxDto> InitializePartCategoriesComboBox()
        {
                IQueryable<PartCategory> partCategories = DatabaseContext.PartCategories.Where(item => item.IsActive);
                IQueryable<ComboBoxDto> comboboxDto = partCategories.Select(item => new ComboBoxDto()
                {
                    Id = item.Id,
                    Title = item.CategoryName,
                });
                return new ObservableCollection<ComboBoxDto>(comboboxDto);
        }
        public int InitializeNumberOfActiveParts()
        {
            return DatabaseContext.Parts.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Part.PartName),
                    DisplayName = "Part Name"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Part.PartCategory.CategoryName),
                    DisplayName = "Part Category"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Part.PartDescription),
                    DisplayName = "Part Description"
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Part.PartName),
                    DisplayName = "Part Name",
                },
                 new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Part.PartCategory),
                    DisplayName = "Category",
                },
                  new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Part.UnitPrice),
                    DisplayName = "Unit Price",
                },
            };
        }
        public override string ValidateProperty(string columnName, Part model)
        {
            if(columnName == nameof(Part.PartName))
            {
                if (string.IsNullOrEmpty(model.PartName))
                {
                    return "Part name is required";
                }
            }
            if (columnName == nameof(Part.UnitPrice))
            {
                if(model.UnitPrice <= 0)
                {
                    return "Price can't be 0 or negative";
                }
            }
            if (columnName == nameof(Part.QuantityInStock))
            {
                if (model.QuantityInStock <= 0)
                {
                    return "Quantity can't be 0 or negative";
                }
            }
            if (columnName == nameof(Part.PartCategoryId))
            {
                if (model.PartCategoryId == default(int))
                {
                    return "Quantity is required";
                }
            }
            return string.Empty;
        }
    }
}

