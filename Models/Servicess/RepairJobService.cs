using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
namespace ComputerRepairService.Models.Servicess
{
    public class RepairJobService : BaseService<RepairJobDto, RepairJob>
    {
        public readonly JobServiceService _JobServiceService;

        public readonly JobPartService _JobPartService;
        public decimal? TotalCostFrom { get; set; }
        public decimal? TotalCostTo { get; set; }
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public RepairJobService()
        {
            //creating two services inside for many to many view
            _JobServiceService = new JobServiceService();
            _JobPartService = new JobPartService();
        }
        public override void AddOrUpdateModel(RepairJob model)
        {
            if (model.Id == default)
            {
                DatabaseContext.RepairJobs.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(RepairJobDto model)
        {
            RepairJob repairJob = DatabaseContext.RepairJobs.First(item => item.Id == model.Id);
            repairJob.IsActive = false;
            repairJob.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override RepairJob GetModel(int id)
        {
            return DatabaseContext.RepairJobs.First(item => item.Id == id);
        }

        public override List<RepairJobDto> GetModels()
        {

            IQueryable<RepairJob> repairJob = DatabaseContext.RepairJobs.Include(item => item.Customer).Include(item => item.JobStatusNavigation).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(RepairJobDto.CustomerName):
                        repairJob = repairJob.Where(item => item.Customer.Surname.Contains(SearchInput) || item.Customer.FirstName.Contains(SearchInput));
                        break;
                    case nameof(RepairJob.JobStatusNavigation.StatusName):
                        repairJob = repairJob.Where(item => item.JobStatusNavigation.StatusName.Contains(SearchInput));
                        break;
                    case nameof(RepairJob.IssueDescription):
                        repairJob = repairJob.Where(item => item.IssueDescription!.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null)
            {
                repairJob = repairJob.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                repairJob = repairJob.Where(item => item.DateCreated <= DateCreatedTo);
            }
            if (TotalCostFrom != null)
            {
                repairJob = repairJob.Where(item => item.TotalCost >= TotalCostFrom);
            }
            if (TotalCostTo != null)
            {
                repairJob = repairJob.Where(item => item.TotalCost <= TotalCostTo);
            }
            IQueryable<RepairJobDto> repairJobDto = repairJob.Select(item => new RepairJobDto()
            {
                Id = item.Id,
                CustomerId = item.CustomerId,
                CustomerName = item.Customer.FirstName + " " + item.Customer.Surname,
                TotalCost = item.TotalCost,
                //enity veirdly mapped foreign key
                JobStatus = item.JobStatusNavigation.StatusName,
                IssueDescription = item.IssueDescription,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
            });
            switch (OrderProperty)
            {
                case nameof(RepairJobDto.CustomerName):
                    repairJobDto = OrderAscending ? repairJobDto.OrderBy(item => item.CustomerName) : repairJobDto.OrderByDescending(item => item.CustomerName);
                    break;
                case nameof(RepairJob.TotalCost):
                    repairJobDto = OrderAscending ? repairJobDto.OrderBy(item => item.TotalCost) : repairJobDto.OrderByDescending(item => item.TotalCost);
                    break;
                case nameof(RepairJob.JobStatusNavigation.StatusName):
                    repairJobDto = OrderAscending ? repairJobDto.OrderBy(item => item.JobStatus) : repairJobDto.OrderByDescending(item => item.JobStatus);
                    break;
            }
            return repairJobDto.ToList();
        }
        public void updateTotalCost(int? repairJobId, decimal cost)
        {
            RepairJob repairJob = DatabaseContext.RepairJobs.First(item => repairJobId == item.Id);
            if (repairJob != null)
            {
                repairJob.TotalCost = cost;
                DatabaseContext.SaveChanges();
            }
        }
        public override void UpdateModel(RepairJob model)
        {
            DatabaseContext.RepairJobs.Update(model);
            DatabaseContext.SaveChanges();
        }
        public override RepairJob CreateModel()
        {
            return new RepairJob()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
                TotalCost = 0,
            };
        }
        //method used for calculation total prices returng tuple of values to view model

        //optimize the code eliminate nullable variables
        //move save changes to some kind of button to relieve databse stress
        public (decimal? TotalCost, decimal? TotalServicesCost, decimal? TotalPartsCost) CalculateTotalCosts(int? repairJobId)
        {
            decimal servicesCost = DatabaseContext.JobServices
            .Where(item => item.IsActive)
            .Where(item => item.JobId == repairJobId.Value)
            .Sum(item => item.Cost);

            decimal? partsCost = DatabaseContext.JobParts
            .Where(item => item.IsActive)
            .Where(item => item.JobId == repairJobId.Value)
            .Sum(item => item.Cost * item.QuantityUsed);

            decimal? totalCost = servicesCost + partsCost;

            RepairJob repairJob = DatabaseContext.RepairJobs.First(item => item.Id == repairJobId.Value);
            repairJob.TotalCost = totalCost;
            DatabaseContext.SaveChanges();
            return (servicesCost, partsCost, totalCost);
        }
        public ObservableCollection<ComboBoxDto> InitializeJobStatusesComboBox()
        {
            IQueryable<ComboBoxDto> JobStatuses = DatabaseContext.JobStatuses.Where(item => item.IsActive).Select(item => new ComboBoxDto()
            {
                Id = item.Id,
                Title = item.StatusName
            });
            return new ObservableCollection<ComboBoxDto>(JobStatuses);
        }
        public string GetCustomerNameById(int customerId)
        {
            //here not checking if the element is active
            return DatabaseContext.Customers
                   .Where(item => item.Id == customerId)
                   .Select(item => item.FirstName + " " + item.Surname)
                   .First();
        }
        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    DisplayName = "Customer",
                    PropertyTitle =  nameof(RepairJobDto.CustomerName)
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Job Status",
                    PropertyTitle =  nameof(RepairJob.JobStatusNavigation.StatusName)
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Issue",
                    PropertyTitle =  nameof(RepairJob.IssueDescription)
                },
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(RepairJobDto.CustomerName),
                    DisplayName = "Customer",
                },
                 new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(RepairJob.TotalCost),
                    DisplayName = "Total Cost",
                },
                  new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(RepairJob.JobStatusNavigation.StatusName),
                    DisplayName = "Status",
                },
            };
        }
    }
}
