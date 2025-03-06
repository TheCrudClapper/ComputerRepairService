using ComputerRepairService.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace ComputerRepairService.Models.Servicess
{
    public class FeedbackService : BaseService<FeedbackDto, Feedback>
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public override void AddOrUpdateModel(Feedback model)
        {
            DatabaseContext.Feedbacks.Add(model);
            DatabaseContext.SaveChanges();
        }

        public override void DeleteModel(FeedbackDto model)
        {
            Feedback feedback = DatabaseContext.Feedbacks.First(item => item.Id == model.Id);
            feedback.IsActive = false;
            feedback.DateDeleted = DateTime.Now;
            DatabaseContext.SaveChanges();
        }

        public override Feedback GetModel(int id)
        {
            return DatabaseContext.Feedbacks.First(item => item.Id == id);
        }

        public override List<FeedbackDto> GetModels()
        {
            IQueryable<Feedback> feedbacks = DatabaseContext.Feedbacks.Include(item => item.Job).ThenInclude(item => item.Customer).Where(item => item.IsActive);
            if (!string.IsNullOrEmpty(SearchInput))
            {
                switch (SearchProperty)
                {
                    case nameof(Feedback.Job.Id):
                        feedbacks = feedbacks.Where(item => item.JobId + "" == SearchInput);
                        break;
                    case nameof(Feedback.Rating):
                        feedbacks = feedbacks.Where(item => item.Rating + "" == SearchInput);
                        break;
                    case nameof(Feedback.FeedbackText):
                        feedbacks = feedbacks.Where(item => item.FeedbackText.Contains(SearchInput));
                        break;
                }
            }
            if (DateCreatedFrom != null)
            {
                feedbacks = feedbacks.Where(item => item.DateCreated >= DateCreatedFrom);
            }
            if (DateCreatedTo != null)
            {
                feedbacks = feedbacks.Where(item => item.DateCreated <= DateCreatedTo);
            }
            IQueryable<FeedbackDto> feedbackDtos = feedbacks.Select(item => new FeedbackDto()
            {
                Id = item.Id,
                JobId = item.JobId,
                Customer = item.Job.Customer.FirstName + " " + item.Job.Customer.Surname,
                IssueDescription = item.Job.IssueDescription,
                FeedbackText = item.FeedbackText,
                Rating = item.Rating,
                DateCreated = item.DateCreated,
                DateEdited = item.DateEdited,

            });
            switch (OrderProperty)
            {
                case nameof(FeedbackDto.Customer):
                    feedbackDtos = OrderAscending ? feedbackDtos.OrderBy(item => item.Customer) : feedbackDtos.OrderByDescending(item => item.Customer);
                    break;
                case nameof(Feedback.Job.Id):
                    feedbackDtos = OrderAscending ? feedbackDtos.OrderBy(item => item.JobId) : feedbackDtos.OrderByDescending(item => item.JobId);
                    break;
                case nameof(Feedback.Rating):
                    feedbackDtos = OrderAscending ? feedbackDtos.OrderBy(item => item.Rating) : feedbackDtos.OrderByDescending(item => item.Rating);
                    break;
                case nameof(Feedback.DateCreated):
                    feedbackDtos = OrderAscending ? feedbackDtos.OrderBy(item => item.DateCreated) : feedbackDtos.OrderByDescending(item => item.DateCreated);
                    break;
            }
            return feedbackDtos.ToList();
        }

        public override Feedback CreateModel()
        {
            return new Feedback()
            {
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public override void UpdateModel(Feedback model)
        {
            DatabaseContext.Feedbacks.Update(model);
            DatabaseContext.SaveChanges();
        }

        public override List<SearchComboBoxDto> GetSearchComboBoxDtos()
        {
            return new List<SearchComboBoxDto>()
            {
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Feedback.Job.Id),
                    DisplayName = "Job ID"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Feedback.Rating),
                    DisplayName = "Rating"
                },
                new SearchComboBoxDto()
                {
                    PropertyTitle = nameof(Feedback.FeedbackText),
                    DisplayName = "Feedback Text"
                }
            };
        }

        public override List<SearchComboBoxDto> GetOrderByComboBoxDtos()
        {
            return new List<SearchComboBoxDto>
            {
                new SearchComboBoxDto()
                {
                    //this one is get from dto
                    DisplayName = "Customer",
                    PropertyTitle = nameof(FeedbackDto.Customer),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Job ID",
                    PropertyTitle = nameof(Feedback.Job.Id),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Rating",
                    PropertyTitle = nameof(Feedback.Rating),
                },
                new SearchComboBoxDto()
                {
                    DisplayName = "Date Created",
                    PropertyTitle = nameof(Feedback.DateCreated),
                },

            };
        }
    }
}
