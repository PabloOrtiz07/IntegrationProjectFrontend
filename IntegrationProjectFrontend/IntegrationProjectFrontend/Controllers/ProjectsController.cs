using Data.Base;
using Data.DTOs;
using IntegrationProjectFrontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace IntegrationProjectFrontend.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public ProjectsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Projects() // Change "Users" to "Projects"
        {
            return View();
        }

        public async Task<IActionResult> ProjectsAddPartial([FromBody] ProjectDTO projectDTO) // Change "UserDTO" to "ProjectDTO"
        {
            var projectsViewModel = new ProjectsViewModel(); // Change "UsersViewModel" to "ProjectsViewModel"

            if (projectDTO != null)
            {
                projectsViewModel = projectDTO;
            }

            return PartialView("~/Views/Projects/Partial/ProjectsAddPartial.cshtml", projectsViewModel);
        }

        public IActionResult RegisterProject(ProjectDTO project) // Change "UserDTO" to "ProjectDTO"
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var projects = baseApi.PostToApi("Projects", project, token); // Change "Users" to "Projects"
            return View("~/Views/Projects/Projects.cshtml"); // Change "Users" to "Projects"
        }

        public IActionResult EditProject(ProjectDTO project) // Change "UserDTO" to "ProjectDTO"
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var id = project.Id;
            var apiUrl = $"Projects/{id}?parameter=0"; // Change "Users" to "Projects"
            var projects = baseApi.PutToApi(apiUrl, project, token); // Change "Users" to "Projects"
            return View("~/Views/Projects/Projects.cshtml"); // Change "Users" to "Projects"
        }
    }
}

