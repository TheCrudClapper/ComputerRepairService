using ComputerRepairService.Models.Dtos;

namespace ComputerRepairService.Models.Servicess
{
    public class JobStatusesService : BaseService<JobStatusDto, JobStatus>
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public override void AddOrUpdateModel(JobStatus model)
        {
            if (model.Id == default)
            {
                DatabaseContext.JobStatuses.Add(model);
                DatabaseContext.SaveChanges();
            }
            else
            {
                UpdateModel(model);
            }
        }

        public override void DeleteModel(JobStatusDto model)
        {
            JobStatus jobStatus = DatabaseContext.JobStatuses.First(item => item.Id == model.Id);
            jobStatus.IsActive = false;
            jobStatus.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override JobStatus GetModel(int id)
        {
            return DatabaseContext.JobStatuses.First(item => item.Id == id);
        }

        public override List<JobStatusDto> GetModels()
        {
            IQueryable<JobStatus> jobStatuses = DatabaseContext.JobStatuses.Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(JobStatus.StatusName):
                        jobStatuses = jobStatuses.Where(item => item.StatusName.Contains(SearchInput));
                        break;
                    case nameof(JobStatus.StatusDescription):
                        jobStatuses = jobStatuses.Where(item => item.StatusDescription.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null)
            {
                jobStatuses = jobStatuses.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                jobStatuses = jobStatuses.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<JobStatusDto> jobStatusDtos = jobStatuses.Select(item => new JobStatusDto()
            {
                Id = item.Id,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,
                StatusDescription = item.StatusDescription,
                StatusName = item.StatusName,
            });
            switch (OrderProperty)
            {
                case nameof(JobStatus.StatusName):
                    jobStatusDtos = OrderAscending ? jobStatusDtos.OrderBy(item => item.StatusName) : jobStatusDtos.OrderByDescending(item => item.StatusName);
                    break;
                case nameof(JobStatus.StatusDescription):
                    jobStatusDtos = OrderAscending ? jobStatusDtos.OrderBy(item => item.StatusDescription) : jobStatusDtos.OrderByDescending(item => item.StatusName);
                    break;
            }
            return jobStatusDtos.ToList();
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(JobStatus.StatusName),
                    DisplayName = "Name",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(JobStatus.StatusDescription),
                    DisplayName = "Description",
                }
            };
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(JobStatus.StatusName),
                    DisplayName = "Name",
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(JobStatus.StatusDescription),
                    DisplayName = "Description",
                }
            };
        }
        public override JobStatus CreateModel()
        {
            return new JobStatus()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }
        public override void UpdateModel(JobStatus model)
        {
            model.DateEdited = DateTime.Now;
            DatabaseContext.JobStatuses.Update(model);
            DatabaseContext.SaveChanges();
        }
        public int GetNumberOfActiveStatuses()
        {
            return DatabaseContext.JobStatuses.Where(item => item.IsActive).Count();
        }
        public override string ValidateProperty(string columnName, JobStatus model)
        {
            if (columnName == nameof(JobStatus.StatusName))
            {
                if (string.IsNullOrWhiteSpace(model.StatusName))
                {
                    return "Status Name is required";
                }
                if (int.TryParse(model.StatusName, out _))
                {
                    return "Status Name can't be number";
                }
                if (model.StatusName.Length < 4)
                {
                    return "Status Name can't be shorter than 4 chars";
                }
            }
            return string.Empty;
        }
    }
}
