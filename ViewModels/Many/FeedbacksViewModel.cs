using ComputerRepairService.Models;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;

namespace ComputerRepairService.ViewModels.Many
{
    public class FeedbacksViewModel : BaseManyViewModel<FeedbackService, FeedbackDto, Feedback>
    {
        public DateTime? DateCreatedFrom
        {
            get => Service.DateCreatedFrom;
            set
            {
                if (Service.DateCreatedFrom != value)
                {
                    Service.DateCreatedFrom = value;
                    OnPropertyChanged(() => DateCreatedFrom);
                }
            }
        }

        public DateTime? DateCreatedTo
        {
            get => Service.DateCreatedTo;
            set
            {
                if (Service.DateCreatedTo != value)
                {
                    Service.DateCreatedTo = value;
                    OnPropertyChanged(() => DateCreatedTo);
                }
            }
        }
        public FeedbacksViewModel() : base("Feedbacks")
        {
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
        }

        protected override void ClearFilters()
        {
            DateCreatedFrom = null;
            DateCreatedTo = null;
            SearchInput = null;
            SearchProperty = SearchComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderProperty = OrderComboBoxDto.FirstOrDefault()?.PropertyTitle;
            OrderAscending = false;
            Refresh();
        }

        protected override void CreateNew()
        {

        }
    }
}
