using ComputerRepairService.Models.BusinessObjects;
using ComputerRepairService.Models.Dtos;
using LiveCharts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerRepairService.Models.Servicess
{
    public class DashboardService : BusinessService
    {
        public ObservableCollection<CardModel> GetCards()
        {
            //generation of card contents
            return new ObservableCollection<CardModel>()
            {
                new CardModel { Description = "Total Amount of Schedules", Value = GetActiveSchedules().ToString(), Title = "Schedules" },
                new CardModel { Description = "Total Completed Repairs", Value = GetCompletedJobs().ToString(), Title = "Completed Jobs" },
                new CardModel { Description = "Total Amount of Customers", Value = GetActiveCustomers().ToString(), Title = "Customers" },
                new CardModel { Description = "Total Amount of Employees", Value = GetActiveEmployees().ToString(), Title = "Employees" },
                new CardModel { Description = "Total Amount of Suppliers", Value = GetActiveSuppliers().ToString(), Title = "Suppliers" },
                new CardModel { Description = "Admitted, Pending Repairs etc.", Value = GetRepairsToComplete().ToString(), Title = "Repairs To Complete" },
            };
        }
        public int GetActiveSchedules()
        {
            return DatabaseContext.Schedules.Where(item => item.IsActive).Count();
        }
        public int GetCompletedJobs()
        {
            //including even those not active
            return DatabaseContext.RepairJobs.Where(item => item.JobStatusNavigation.StatusName == "Finished").Count();
        }
        public int GetActiveCustomers()
        {
            return DatabaseContext.Customers.Where(item => item.IsActive).Count();
        }
        public int GetActiveEmployees()
        {
            return DatabaseContext.Employees.Where(item => item.IsActive).Count();
        }
        public int GetActiveSuppliers()
        {
            return DatabaseContext.Suppliers.Where(item => item.IsActive).Count();
        }
        public int GetRepairsToComplete()
        {
            //list to hold valid statuses to return
            List<string> validStatuses = new List<string>()
            {
                "Admitted",
                "Part Complementation",
                "Pending"
            };
            //return statuses defined in array
            return DatabaseContext.RepairJobs
                .Where(item => item.IsActive && validStatuses.Contains(item.JobStatusNavigation.StatusName)).Count();
        }
        public List<ChartDto> GetRepairsInTime()
        {
            //date today
            DateTime dateTo = DateTime.Today;
            //date from six months
            DateTime dateFrom = dateTo.AddMonths(-6);
            //Getting schedules in time period
            IQueryable<Schedule> schedules = DatabaseContext.Schedules.Where(item => item.EndDate >= dateFrom && item.EndDate <= dateTo);

            List<ChartDto> repairsGroupedByMonth = schedules
              .Where(item => item.EndDate.HasValue) 
              //here doing it o c# level not on database
              .AsEnumerable()
              //using value here because end date is of type datetime?
              .GroupBy(item => item.EndDate!.Value.ToString("yyyy-MM"))
              //getting values into dto
              .Select(dto => new ChartDto()
              {
                  Label = dto.Key,
                  Value = dto.Count()
              })
              //ordering by month
              .OrderBy(dto => dto.Label)
              .ToList();
            //returning list
            return repairsGroupedByMonth;
        }



        //methods return average of feedbacks ordered by months (perioid 6 months)
        public List<ChartDto> GetAvgFeedbackValueInTime()
        {
            DateTime dateTo = DateTime.Today;
            DateTime dateFrom = dateTo.AddMonths(-6);

            IQueryable<ChartDto> feedbacksAvgPerMonth = DatabaseContext.Feedbacks
                //selecting period
                .Where(item => item.DateCreated >= dateFrom && item.DateCreated <= dateTo)
                //grouping by date
                .GroupBy(item => new { item.DateCreated.Year, item.DateCreated.Month })
                .Select(item => new ChartDto
                {
                    //assigning the to achieve format of date yyyy-MM
                    Label = item.Key.Year + "-" + item.Key.Month,
                    //assigning average of rating to value
                    Value = Math.Round(item.Average(f => f.Rating),2)
                })
                //ordering by label (year - month)
                .OrderBy(item => item.Label);
                //returning 
                return feedbacksAvgPerMonth.ToList();
        }
    }
}

