﻿namespace ComputerRepairService.Models.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? ServiceDescription { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
