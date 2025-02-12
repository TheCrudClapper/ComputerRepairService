using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace ComputerRepairService.Models.Servicess
{
    public class PartCategoryService : BaseService<PartCategoryDto, PartCategory>
    {
        public bool HasDescription { get; set; }
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo {  get; set; }

        public override void AddOrUpdateModel(PartCategory model)
        {
            if (model.Id == default)
            {
                DatabaseContext.PartCategories.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(PartCategoryDto model)
        {
            PartCategory partCategory = DatabaseContext.PartCategories.First(item => item.Id == model.Id);
            partCategory.IsActive = false;
            partCategory.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override PartCategory GetModel(int id)
        {
            return DatabaseContext.PartCategories.First(item => item.Id == id);
        }

        public override List<PartCategoryDto> GetModels()
        {
            IQueryable<PartCategory> partCategories = DatabaseContext.PartCategories.Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(PartCategory.CategoryName):
                        partCategories = partCategories.Where(item => item.CategoryName + "" == SearchInput);
                        break;
                    case nameof(PartCategory.CategoryDescription):
                        partCategories = partCategories.Where(item => item.CategoryDescription.Contains(SearchInput));
                        break;
                    case nameof(PartCategory.Id):
                        partCategories = partCategories.Where(item => item.Id + ""  == SearchInput);
                        break;
                }
            }
            if (HasDescription)
            {
                partCategories = partCategories.Where(item => !string.IsNullOrEmpty(item.CategoryDescription));
            }
            if (DateCreatedFrom != null)
            {
                partCategories = partCategories.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                partCategories = partCategories.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<PartCategoryDto> partCategoryDtos = partCategories.Select(item => new PartCategoryDto()
            {
                Id = item.Id,
                CategoryDescription = item.CategoryDescription,
                CategoryName = item.CategoryName,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
            });
            switch (OrderProperty)
            {
                case nameof(PartCategory.CategoryName):
                    partCategoryDtos = OrderAscending ?  partCategoryDtos.OrderBy(item => item.CategoryName) : partCategoryDtos.OrderByDescending(item => item.CategoryName);
                    break;
                case nameof(PartCategory.DateCreated):
                    partCategoryDtos = OrderAscending ? partCategoryDtos.OrderBy(item => item.DateCreated) : partCategoryDtos.OrderByDescending(item => item.DateCreated);
                    break;
                case nameof(PartCategory.CategoryDescription):
                    partCategoryDtos = OrderAscending ? partCategoryDtos.OrderBy(item => item.CategoryDescription) : partCategoryDtos.OrderByDescending(item => item.CategoryDescription);
                    break;
            }
            return partCategoryDtos.ToList();
        }

        public override PartCategory CreateModel()
        {
            return new PartCategory()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(PartCategory model)
        {
            DatabaseContext.PartCategories.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int InitializeNumberOfActivePartCategories()
        {
            return DatabaseContext.PartCategories.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartCategory.CategoryName),
                    DisplayName = "Category Name"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartCategory.CategoryDescription),
                    DisplayName = "Description"
                },
                 new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartCategory.Id),
                    DisplayName = "Category ID"
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartCategory.CategoryName),
                    DisplayName = "Category Name"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartCategory.DateCreated),
                    DisplayName = "Date Created"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(PartCategory.CategoryDescription),
                    DisplayName = "Description"
                },
            };
        }
        public override string ValidateProperty(string columnName, PartCategory model)
        {
            if(columnName == nameof(PartCategory.CategoryName))
            {
                if (string.IsNullOrWhiteSpace(model.CategoryName))
                {
                    return "Category Name is required";
                }
                if (model.CategoryName.Length < 3)
                {
                    return "Category Name must be longer than 3 chars";
                }
                if (int.TryParse(model.CategoryName, out _))
                {
                    return "Category Name can't be number";
                }
            }
            return string.Empty;
        }
    }
}
