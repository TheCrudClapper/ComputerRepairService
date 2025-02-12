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
    public class RoleService : BaseService<RoleDto, Role>
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public override void AddOrUpdateModel(Role model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Roles.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(RoleDto model)
        {
            Role role = DatabaseContext.Roles.First(item => item.Id == model.Id);
            role.IsActive = false;
            role.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Role GetModel(int id)
        {
            return DatabaseContext.Roles.First(item => item.Id == id);
        }

        public override List<RoleDto> GetModels()
        {
            IQueryable<Role> roles = DatabaseContext.Roles.Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Role.RoleName):
                        roles = roles.Where(item => item.RoleName.Contains(SearchInput));
                        break;
                    case nameof(Role.RoleDescription):
                        roles = roles.Where(item => item.RoleDescription.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null) 
            {
                roles = roles.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                roles = roles.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<RoleDto> roleDtos = roles.Select(item => new RoleDto()
            {
                Id = item.Id,
                RoleName = item.RoleName,
                RoleDescription = item.RoleDescription,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
            });
            switch (OrderProperty)
            {
                case nameof(Role.RoleName):
                    roleDtos = OrderAscending ? roleDtos.OrderBy(item => item.RoleName) : roleDtos.OrderByDescending(item => item.RoleName);
                    break;
                case nameof(Role.DateCreated):
                    roleDtos = OrderAscending ? roleDtos.OrderBy(item => item.DateCreated) : roleDtos.OrderByDescending(item => item.DateCreated);
                    break;
            }
            return roleDtos.ToList();
        }

        public override Role CreateModel()
        {
            return new Role()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(Role model)
        {
            DatabaseContext.Roles.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int InitializeNumberOfActiveRoles()
        {
            //returns number of roles to display on adding window
            return DatabaseContext.Roles.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto
                {
                    PropertyTitle = nameof(Role.RoleName),
                    DisplayName = "Name"
                },
                new SearchComboBoxDto
                {
                    PropertyTitle = nameof(Role.RoleDescription),
                    DisplayName = "Description"
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
           {
               new SearchComboBoxDto()
               {
                    PropertyTitle= nameof(Role.RoleName),
                    DisplayName = "Name",
               },
                new SearchComboBoxDto()
               {
                    PropertyTitle= nameof(Role.DateCreated),
                    DisplayName = "Date Created",
               },

           };
        }
        public override string ValidateProperty(string columnName, Role model)
        {
            if (columnName == nameof(Role.RoleName))
            {
                if (string.IsNullOrWhiteSpace(model.RoleName))
                {
                    return "Role Name is required";
                }
                if (int.TryParse(model.RoleName, out _))
                {
                    return "Role Name can't be number";
                }
                if(model.RoleName.Length < 4)
                {
                    return "Role name can't be shorter than 4 chars";
                }
            }
            return string.Empty;
        }
    }
}
