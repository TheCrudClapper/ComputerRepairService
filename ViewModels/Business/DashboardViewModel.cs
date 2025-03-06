using ComputerRepairService.Models.BusinessObjects;
using ComputerRepairService.Models.Dtos;
using ComputerRepairService.Models.Servicess;
using LiveCharts;
using System.Collections.ObjectModel;

namespace ComputerRepairService.ViewModels.Business
{
    public class DashboardViewModel : WorkspaceViewModel
    {
        //defining serivce
        private DashboardService _dashboardService;
        private List<ChartDto> RepairsAndMoths { get; set; }
        public ChartValues<int> Repairs { get; set; }
        public List<string> Months { get; set; }
        private List<ChartDto> AverageFeedbacksPerMonth { get; set; }
        public ChartValues<double> FeedbacksAvg { get; set; }
        public List<string> FeedbackMonths { get; set; }
        private ObservableCollection<CardModel> _Cards { get; set; }
        public ObservableCollection<CardModel> Cards
        {
            get => _Cards;
            set
            {
                if (_Cards != value)
                {
                    _Cards = value;
                    OnPropertyChanged(() => Cards);
                }
            }
        }
        public DashboardViewModel() : base("Dashboard")
        {
            //assigning service
            _dashboardService = new DashboardService();
            Cards = _dashboardService.GetCards();
            //list that holds datafrom service
            RepairsAndMoths = _dashboardService.GetRepairsInTime();
            AverageFeedbacksPerMonth = _dashboardService.GetAvgFeedbackValueInTime();
            //splitting data into values and labels for charts
            //casting double to int for repairs value
            Repairs = new ChartValues<int>(RepairsAndMoths.Select(item => (int)item.Value));
            Months = RepairsAndMoths.Select(item => item.Label).ToList();
            FeedbackMonths = RepairsAndMoths.Select(item => item.Label).ToList();
            FeedbacksAvg = new ChartValues<double>(AverageFeedbacksPerMonth.Select(item => item.Value));
        }

    }
}
