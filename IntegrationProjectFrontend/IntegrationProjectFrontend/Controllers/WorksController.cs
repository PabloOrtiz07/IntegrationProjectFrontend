using Data.Base;
using Data.DTOs;
using IntegrationProjectFrontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace IntegrationProjectFrontend.Controllers
{
    [Authorize]
    public class WorksController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public WorksController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Works() // Change "Users" to "Works"
        {
            return View();
        }

        public async Task<IActionResult> WorksAddPartial([FromBody] WorkDTO workDTO) // Change "UserDTO" to "WorkDTO"
        {
            var worksViewModel = new WorksViewModel(); // Change "UsersViewModel" to "WorksViewModel"

            if (workDTO != null)
            {
                worksViewModel = workDTO;
            }

            return PartialView("~/Views/Works/Partial/WorksAddPartial.cshtml", worksViewModel);
        }

        public IActionResult RegisterWork(WorkDTO work) // Change "UserDTO" to "WorkDTO"
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var works = baseApi.PostToApi("Works", work, token); // Change "Users" to "Works"
            return View("~/Views/Works/Works.cshtml"); // Change "Users" to "Works"
        }

        public IActionResult EditWork(WorkDTO work) // Change "UserDTO" to "WorkDTO"
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var id = work.Id;
            var apiUrl = $"Works/{id}?parameter=0"; // Change "Users" to "Works"
            var works = baseApi.PutToApi(apiUrl, work, token); // Change "Users" to "Works"
            return View("~/Views/Works/Works.cshtml"); // Change "Users" to "Works"
        }
    }

}
