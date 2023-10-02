using System;

namespace Data.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
