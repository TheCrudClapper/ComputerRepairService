using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using ComputerRepairService.Models.Contexts;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
namespace ComputerRepairService.Models.Servicess
{
    public class EmployeeService : BaseService<EmployeeDto, Employee>
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public bool HasRole { get; set; }
        public override void AddOrUpdateModel(Employee model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Employees.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }

        }

        public override void DeleteModel(EmployeeDto model)
        {
            Employee employee = DatabaseContext.Employees.First(item => item.Id == model.Id);
            employee.IsActive = false;
            employee.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Employee GetModel(int id)
        {
            return DatabaseContext.Employees.Include(item => item.Role).First(item => item.Id == id);
        }

        public override List<EmployeeDto> GetModels()
        {
            IQueryable<Employee> employees = DatabaseContext.Employees.Include(item => item.Role).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Employee.FirstName):
                        employees = employees.Where(item => item.FirstName.Contains(SearchInput));
                        break;
                    case nameof(Employee.Role.RoleName):
                        employees = employees.Where(item => item.Role.RoleName.Contains(SearchInput));
                        break;
                    case nameof(Employee.PhoneNumber):
                        employees = employees.Where(item => item.PhoneNumber.Contains(SearchInput));
                        break;
                    case nameof(Employee.Email):
                        employees = employees.Where(item => item.Email.Contains(SearchInput));
                        break;
                }

            }
            if (HasRole) 
            {
                employees = employees.Where(item => item.Role != null);
            }
            if (DateCreatedFrom != null)
            {
                employees = employees.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                employees = employees.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<EmployeeDto> employeDto = employees.Select(item => new EmployeeDto()
            {
                Id = item.Id,
                Surname = item.Surname,
                FirstName = item.FirstName,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                Email = item.Email,
                HireDate = item.HireDate,
                Position = item.Role!.RoleName,
                PhoneNumber = item.PhoneNumber,
                Salary = item.Salary
            });
            switch (OrderProperty)
            {
                case nameof(Employee.FirstName):
                    employeDto = OrderAscending ? employeDto.OrderBy(item => item.FirstName) : employeDto.OrderByDescending(item => item.FirstName);
                    break;
                case nameof(Employee.Surname):
                    employeDto = OrderAscending ? employeDto.OrderBy(item => item.Surname) : employeDto.OrderByDescending(item => item.Surname);
                    break;
                case nameof(Employee.Role):
                    employeDto = OrderAscending ? employeDto.OrderBy(item => item.Position) : employeDto.OrderByDescending(item => item.Position);
                    break;
                case nameof(Employee.HireDate):
                    employeDto = OrderAscending ? employeDto.OrderBy(item => item.HireDate) : employeDto.OrderByDescending(item => item.HireDate);
                    break;
            }
            return employeDto.ToList();
        }
        public override Employee CreateModel()
        {
            return new Employee()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }
        public override void UpdateModel(Employee model)
        {
            DatabaseContext.Employees.Update(model);
            DatabaseContext.SaveChanges();
        }

        public ObservableCollection<ComboBoxDto> InitializeRolesComboBox()
        {
            IQueryable<ComboBoxDto> comboBoxDto = DatabaseContext.Roles.Where(item => item.IsActive).Select(item => new ComboBoxDto()
            {
                Id = item.Id,
                Title = item.RoleName,
            });
            return new ObservableCollection<ComboBoxDto>(comboBoxDto.ToList());
        }
        public int InitializeNumberOfActiveEmployees()
        {
            return DatabaseContext.Employees.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Employee.FirstName),
                    DisplayName = "First Name"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Employee.Role.RoleName),
                    DisplayName = "Role Name"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Employee.PhoneNumber),
                    DisplayName = "Phone Number"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Employee.Email),
                    DisplayName = "Email"
                }
            };

        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    DisplayName = "Name",
                    PropertyTitle = nameof(Employee.FirstName),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Surname",
                    PropertyTitle = nameof(Employee.Surname),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Hire Date",
                    PropertyTitle = nameof(Employee.HireDate),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Position",
                    PropertyTitle = nameof(Employee.Role),
                },

            };
        }
        public override string ValidateProperty(string columnName, Employee model)
        {
            if (columnName == nameof(Employee.FirstName))
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                {
                    return "First Name is required !";
                }
            }
            else if (columnName == nameof(Employee.Surname))
            {
                if (string.IsNullOrWhiteSpace(model.Surname))
                {
                    return "Surname is required !";
                }
            }
            else if (columnName == nameof(Employee.RoleId))
            {
                if (model.RoleId == null)
                {
                    return "Role is required";
                }
            }
            else if (columnName == nameof(Employee.HireDate))
            {
                if (model.HireDate == default(DateOnly))
                {
                    return "Hire Date is required";
                }
                //can't put date in past
                if(model.HireDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    return "Date can't be in past";
                }
            }
            else if (columnName == nameof(Employee.Salary))
            {
                if (model.Salary == default)
                {
                    return "Salary can't be 0";
                }
                if(model.Salary < 4000)
                {
                    return "Salary can't be lower than 4000";
                }
            }
            else if (columnName == nameof(Employee.Email))
            {
                if (model.Email != null)
                {
                    if (!model.Email.Contains("@"))
                    {
                        return "Please, provide valid email";
                    }
                }
            }
            else if(columnName == nameof(Employee.PhoneNumber))
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
            return string.Empty;
        }

        }
    }

