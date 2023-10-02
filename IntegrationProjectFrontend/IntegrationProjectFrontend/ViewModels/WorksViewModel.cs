using Data.DTOs; // Ensure you have the correct using statement

namespace IntegrationProjectFrontend.ViewModels
{
    public class WorksViewModel // Change class name to WorksViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } // Update field name to match WorkDTO

        public int HoursQuantity { get; set; } // Update field name to match WorkDTO

        public double HourlyRate { get; set; } // Update field name to match WorkDTO

        public double Cost { get; set; } // Update field name to match WorkDTO

        public int ProjectId { get; set; } // Update field name to match WorkDTO

        public int ServiceId { get; set; } // Update field name to match WorkDTO

        public static implicit operator WorksViewModel(WorkDTO work)
        {
            var worksViewModel = new WorksViewModel();
            worksViewModel.Id = work.Id;
            worksViewModel.Date = work.Date; // Update field assignment to match WorkDTO
            worksViewModel.HoursQuantity = work.HoursQuantity; // Update field assignment to match WorkDTO
            worksViewModel.HourlyRate = work.HourlyRate; // Update field assignment to match WorkDTO
            worksViewModel.Cost = work.Cost; // Update field assignment to match WorkDTO
            worksViewModel.ProjectId = work.ProjectId; // Update field assignment to match WorkDTO
            worksViewModel.ServiceId = work.ServiceId; // Update field assignment to match WorkDTO
            return worksViewModel;
        }
    }
}
