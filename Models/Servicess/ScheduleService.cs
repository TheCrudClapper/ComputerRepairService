using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace ComputerRepairService.Models.Servicess
{
    public class ScheduleService : BaseService<ScheduleDto, Schedule>
    {
        public bool HasEndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateDue { get; set; }
        public override void AddOrUpdateModel(Schedule model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Schedules.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(ScheduleDto model)
        {
            Schedule schedule = DatabaseContext.Schedules.First(item => item.Id == model.Id);
            schedule.IsActive = false;
            schedule.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Schedule GetModel(int id)
        {
            return DatabaseContext.Schedules.First(item => item.Id == id);
        }

        public override List<ScheduleDto> GetModels()
        {
            IQueryable<Schedule> schedules = DatabaseContext.Schedules.Include(item => item.Employee).Include(item => item.Job).ThenInclude(item => item.Customer).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Schedule.Employee.Surname):
                        schedules = schedules.Where(item => item.Employee.Surname.Contains(SearchInput));
                        break;
                    case nameof(Schedule.Job.Id):
                        schedules = schedules.Where(item => item.Job.Id + "" == SearchInput);
                        break;
                    case nameof(Schedule.Job.IssueDescription):
                        schedules = schedules.Where(item => item.Job.IssueDescription.Contains(SearchInput));
                        break;
                }

            }
            if (HasEndDate)
            {
                schedules = schedules.Where(item => item.EndDate != null);
            }
            if (DateDue != null)
            {
                schedules = schedules.Where(item => item.DateAssigned <= DateDue);
            }
            if (StartDate != null)
            {
                schedules = schedules.Where(item => item.StartDate >= StartDate);
            }
            if (EndDate != null)
            {
                schedules = schedules.Where(item => item.EndDate <= EndDate);
            }
            IQueryable<ScheduleDto> scheduleDtos = schedules.Select(item => new ScheduleDto()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                DateAssigned = item.DateAssigned,
                DateDeleted = item.DateDeleted,
                IssueDescription = item.Job.IssueDescription,
                EmployeeId = item.EmployeeId,
                CustomerName = item.Job.Customer.FirstName + " " +  item.Job.Customer.Surname,
                EmployeeName = item.Employee.FirstName + " " + item.Employee.Surname,
                EndDate = item.EndDate,
                StartDate = item.StartDate,
                JobId = item.JobId,
                
            });
            switch (OrderProperty)
            {
                case nameof(Schedule.DateAssigned):
                    scheduleDtos = OrderAscending ? scheduleDtos.OrderBy(item => item.DateAssigned) : scheduleDtos.OrderByDescending(item => item.DateAssigned);
                    break;
                case nameof(ScheduleDto.EmployeeName):
                    scheduleDtos = OrderAscending ? scheduleDtos.OrderBy(item => item.EmployeeName) : scheduleDtos.OrderByDescending(item => item.EmployeeName);
                    break;
                case nameof(ScheduleDto.CustomerName):
                    scheduleDtos = OrderAscending ? scheduleDtos.OrderBy(item => item.CustomerName) : scheduleDtos.OrderByDescending(item => item.CustomerName);
                    break;
                case nameof(Schedule.Job.Id):
                    scheduleDtos = OrderAscending ? scheduleDtos.OrderBy(item => item.JobId) : scheduleDtos.OrderByDescending(item => item.JobId);
                    break;
            }
            return scheduleDtos.ToList();
        }
        public override Schedule CreateModel()
        {
            return new Schedule()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }
        public override void UpdateModel(Schedule model)
        {
            DatabaseContext.Schedules.Update(model);
            DatabaseContext.SaveChanges();
        }
        public ObservableCollection<ComboBoxDto> InitializeEmployeesComboBox()
        {
            IQueryable<ComboBoxDto> comboBoxDto = DatabaseContext.Employees.Where(item => item.RoleId == 1).Where(item => item.IsActive).Select(item => new ComboBoxDto()
            {
                Id = item.Id,
                Title = item.FirstName + " " + item.Surname
            });
            return new ObservableCollection<ComboBoxDto>(comboBoxDto.ToList());
        }
        public int InitializeNumberOfActiveSchedules()
        {
            return DatabaseContext.Schedules.Where(item => item.IsActive).Count();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Schedule.Employee.Surname),
                    DisplayName = "Employee Surname"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Schedule.Job.Id),
                    DisplayName = "Job ID"
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Payment Status",
                    PropertyTitle = nameof(Schedule.Job.IssueDescription)
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Schedule.DateAssigned),
                    DisplayName = "Date Due",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(ScheduleDto.EmployeeName),
                    DisplayName = "Employee",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(ScheduleDto.CustomerName),
                    DisplayName = "Customer",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Schedule.Job.Id),
                    DisplayName = "Job ID",
                },
                
            };
        }
    }
}
