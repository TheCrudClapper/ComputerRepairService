using ComputerRepairService.Models.Contexts;

namespace ComputerRepairService.Models.Servicess
{
    public class BusinessService
    {
        protected DatabaseContext DatabaseContext { get; set; }
        public BusinessService()
        {
            DatabaseContext = new DatabaseContext();
        }
    }
}
