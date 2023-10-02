using Data.DTOs; // Ensure you have the correct using statement

namespace IntegrationProjectFrontend.ViewModels
{
    public class ProjectsViewModel // Change class name to ProjectsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } // Update field name from FirstName to Name

        public string Address { get; set; } // Add Address field

        public String Status { get; set; } // Add Status field

        public static implicit operator ProjectsViewModel(ProjectDTO project)
        {
            var projectsViewModel = new ProjectsViewModel();
            projectsViewModel.Id = project.Id;
            projectsViewModel.Name = project.Name;
            projectsViewModel.Address = project.Address;
            projectsViewModel.Status = project.Status;
            return projectsViewModel;
        }
    }
}
