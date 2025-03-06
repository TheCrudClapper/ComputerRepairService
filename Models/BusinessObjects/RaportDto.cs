namespace ComputerRepairService.Models.BusinessObjects
{
    public class RaportDto
    {
        public int TotalCustomers { get; set; }
        public int TotalEmployees { get; set; }
        public int ActiveRepairs { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public decimal AverageInvoiceValue { get; set; }
        public int TotalInvoices { get; set; }
        public decimal TotalPaidInvoices { get; set; }
        public int TotalOrderedParts { get; set; }
        public double AverageRating { get; set; }
        public int TotalScheduledJobs { get; set; }
    }
}
