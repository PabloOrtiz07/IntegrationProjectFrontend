using Data.DTOs;

namespace IntegrationProjectFrontend.ViewModels
{
    public class ServicesViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal HourlyRate { get; set; }

        public static implicit operator ServicesViewModel(ServiceDTO service)
        {
            var viewModel = new ServicesViewModel();
            viewModel.Id = service.Id;
            viewModel.Description = service.Description;
            viewModel.IsActive = service.IsActive;
            viewModel.HourlyRate = service.HourlyRate;
            return viewModel;
        }
    }
}
