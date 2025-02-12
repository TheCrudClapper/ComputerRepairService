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
    public class InvoiceService : BaseService<InvoiceDto, Invoice>
    {
        public readonly JobServiceService JobServiceService;
        public readonly JobPartService JobPartService;
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public decimal? TotalCostFrom { get; set; }
        public decimal? TotalCostTo { get; set; }
        public InvoiceService()
        {
            JobPartService = new JobPartService();
            JobServiceService = new JobServiceService();
        }
        public override void AddOrUpdateModel(Invoice model)
        {
            if (model.Id == default)
            {
                DatabaseContext.Invoices.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(InvoiceDto model)
        {
            Invoice invoice = DatabaseContext.Invoices.First(item => item.Id == model.Id);
            invoice.IsActive = false;
            invoice.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Invoice GetModel(int id)
        {
            return DatabaseContext.Invoices.First(item => item.Id == id);
        }

        public override List<InvoiceDto> GetModels()
        {
            IQueryable<Invoice> invoices = DatabaseContext.Invoices.Include(item => item.Job).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Invoice.JobId):
                        invoices = invoices.Where(item => item.JobId + "" == SearchInput);
                        break;
                    case nameof(Invoice.Job.Customer.Surname):
                        invoices = invoices.Where(item => item.Job.Customer.Surname.Contains(SearchInput));
                        break;
                    case nameof(Invoice.PaymentStatus):
                        invoices = invoices.Where(item => item.PaymentStatus.Contains(SearchInput));
                        break;
                    case nameof(Invoice.Job.IssueDescription):
                        invoices = invoices.Where(item => item.Job.IssueDescription.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null)
            {
                invoices = invoices.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                invoices = invoices.Where(item => item.DateCreated <= DateCreatedTo);
            }
            if (TotalCostFrom != null)
            {
                invoices = invoices.Where(item => item.TotalCost >= TotalCostFrom);
            }
            if (TotalCostTo != null)
            {
                invoices = invoices.Where(item => item.TotalCost <= TotalCostTo);
            }
            IQueryable<InvoiceDto> invoicesDto = invoices.Select(item => new InvoiceDto()
            {
                Id = item.Id,
                JobId = item.JobId,
                Customer = item.Job.Customer.FirstName + " " + item.Job.Customer.Surname,
                IssueDate = item.DateOfIssue,
                IssueDescription = item.Job.IssueDescription,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                PaymentStatus = item.PaymentStatus,
                TotalCost = item.TotalCost
            });
            switch (OrderProperty)
            {
                case nameof(InvoiceDto.Customer):
                    invoicesDto = OrderAscending ? invoicesDto.OrderBy(item => item.Customer) : invoicesDto.OrderByDescending(item => item.Customer);
                    break;
                case nameof(InvoiceDto.PaymentStatus):
                    invoicesDto = OrderAscending ? invoicesDto.OrderBy(item => item.PaymentStatus) : invoicesDto.OrderByDescending(item => item.PaymentStatus);
                    break;
                case nameof(InvoiceDto.IssueDate):
                    invoicesDto = OrderAscending ? invoicesDto.OrderBy(item => item.IssueDate) : invoicesDto.OrderByDescending(item => item.IssueDate);
                    break;

            }
            return invoicesDto.ToList();
        }

        public override Invoice CreateModel()
        {
            return new Invoice()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(Invoice model)
        {
            DatabaseContext.Invoices.Update(model);
            DatabaseContext.SaveChanges();
        }
        public (decimal TotalCost, decimal? TotalPartsCost, decimal TotalServicesCost) CalculateTotalCosts(int RepairJobId)
        {
            decimal totalServicesCost = DatabaseContext.JobServices
            .Where(item => item.IsActive)
            .Where(item => item.JobId == RepairJobId)
            .Sum(item => item.Cost);

            decimal? totalPartsCost = DatabaseContext.JobParts
            .Where(item => item.IsActive)
            .Where(item => item.JobId == RepairJobId)
            .Sum(item => item.Cost);

            decimal? totalCost = totalServicesCost + totalPartsCost;
            return(totalCost.GetValueOrDefault(), totalPartsCost, totalServicesCost);
        }
        public ObservableCollection<string> InitializePaymentStatuses()
        {
            return new ObservableCollection<string>()
            {
                "Pending",
                "Completed",
                "Failed",
                "Declined",
                "Refunded",
            };
        }
        public CustomerInvoiceDto GetCustomerDetailsById(int RepairJobId)
        {
            return DatabaseContext.RepairJobs
            .Where(item => item.IsActive)
            .Where(item => item.Id == RepairJobId)
            .Include(item => item.Customer)
            .ThenInclude(item => item.Address)
            .Select(item => new CustomerInvoiceDto()
            {
                Id = item.Customer.Id,
                FirstName = item.Customer.FirstName,
                Surname = item.Customer.Surname,
                PhoneNumber = item.Customer.PhoneNumber,
                Email = item.Customer.Email,
                Nip = item.Customer.Nip,
                Place = item.Customer.Address.Place,
                Street = item.Customer.Address.Street,
            }).First();

        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Invoice.JobId),
                    DisplayName = "Job Id"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Invoice.Job.Customer.Surname),
                    DisplayName = "Customer Surname"
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Payment Status",
                    PropertyTitle = nameof(Invoice.PaymentStatus)
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Invoice.Job.IssueDescription),
                    DisplayName = "Issue",
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    DisplayName = "Customer",
                    PropertyTitle = nameof(InvoiceDto.Customer),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Payment Status",
                    PropertyTitle = nameof(InvoiceDto.PaymentStatus),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Issue Date",
                    PropertyTitle = nameof(InvoiceDto.IssueDate),
                },

            };
        }
        public override string ValidateProperty(string columnName, Invoice model)
        {
            if(columnName == nameof(Invoice.DateOfIssue))
            {
                if(model.DateOfIssue == default(DateTime))
                {
                    return "DateOfIssue is required";
                }
                if (model.DateOfIssue < DateTime.Today)
                {
                    return "Can't set date in past";
                }
            }
            if(columnName == nameof(Invoice.PaymentStatus))
            {
                if (string.IsNullOrWhiteSpace(model.PaymentStatus))
                {
                    return "Payment Status is required";
                }
            }
            if(columnName == nameof(Invoice.TotalCost))
            {
                if(model.TotalCost == default)
                {
                    return "Total cost is required";
                }
                if (model.TotalCost < 0)
                {
                    return "Total can't be negative";
                }
            }
            return string.Empty;
        }
    }
}
