using ComputerRepairService.Models.BusinessObjects;
using System;
using System.Linq;

namespace ComputerRepairService.Models.Servicess
{
    public class RaportService : BusinessService
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IsAll { get; set; }

        public RaportDto GetRaportDto()
        {
            
            IQueryable<RepairJob> repairJobs = DatabaseContext.RepairJobs.Where(item => item.IsActive);
            IQueryable<Invoice> invoices = DatabaseContext.Invoices.Where(item => item.IsActive);
            IQueryable<Feedback> feedback = DatabaseContext.Feedbacks.Where(item => item.IsActive);
            IQueryable<PartOrder> partOrders = DatabaseContext.PartOrders.Where(item => item.IsActive);
            IQueryable<Employee> employee = DatabaseContext.Employees.Where(item => item.IsActive);
            IQueryable<Customer> customers = DatabaseContext.Customers.Where(item => item.IsActive);
            IQueryable<Schedule> schedules = DatabaseContext.Schedules.Where(item => item.IsActive);
            if (!IsAll)
            {
                if (DateFrom != null)
                {
                    repairJobs = repairJobs.Where(item => item.DateCreated >= DateFrom);
                    invoices = invoices.Where(item => item.DateOfIssue >= DateFrom);
                    feedback = feedback.Where(item => item.DateCreated >= DateFrom);
                    partOrders = partOrders.Where(item => item.DateCreated >= DateFrom);
                    employee = employee.Where(item => item.DateCreated >= DateFrom);
                    customers = customers.Where(item => item.DateCreated >= DateFrom);
                    schedules = schedules.Where(item => item.DateCreated >= DateFrom);
                }

                if (DateTo != null)
                {
                    repairJobs = repairJobs.Where(item => item.DateCreated <= DateTo);
                    invoices = invoices.Where(item => item.DateOfIssue <= DateTo);
                    feedback = feedback.Where(item => item.DateCreated <= DateTo);
                    partOrders = partOrders.Where(item => item.DateCreated <= DateTo);
                    employee = employee.Where(item => item.DateCreated <= DateTo);
                    customers = customers.Where(item => item.DateCreated <= DateTo);
                    schedules = schedules.Where(item => item.DateCreated <= DateTo);
                }
            }
            return new RaportDto
            {
                //initializing fields of dto
                TotalCustomers = customers.Count(),
                TotalEmployees = employee.Count(),
                TotalInvoices = invoices.Count(),
                ActiveRepairs = repairJobs.Count(),
                TotalOrderedParts = partOrders.Count(),
                //when there is no invoices we must use check becaus average will throw exception
                AverageInvoiceValue = invoices.Any() ? invoices.Average(item => item.TotalCost) : 0,
                //summing total cost of invoices
                TotalInvoiceValue = invoices.Sum(item => (decimal)item.TotalCost),
                TotalPaidInvoices = invoices.Where(item => item.PaymentStatus == "Completed").Count(),
                AverageRating = feedback.Any() ? feedback.Average(item => item.Rating) : 0,
                TotalScheduledJobs = schedules.Count(),
            };
        }
    }
}
