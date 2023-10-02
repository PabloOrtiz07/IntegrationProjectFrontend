using Data.Base;
using Data.DTOs;
using IntegrationProjectFrontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace IntegrationProjectFrontend.Controllers
{

    [Authorize]
    public class ServicesController : Controller
    {

        private readonly IHttpClientFactory _httpClient;
        public ServicesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Services()
        {
            return View();
        }

        public async Task<IActionResult> ServicesAddPartial([FromBody] ServiceDTO serviceDTO)
        {
            var servicesViewModel = new ServicesViewModel();

            if (serviceDTO != null)
            {
                servicesViewModel = serviceDTO;
            }

            return PartialView("~/Views/Services/Partial/ServicesAddPartial.cshtml", servicesViewModel);
        }

        public IActionResult RegisterService(ServiceDTO service)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var services = baseApi.PostToApi("Services", service, token);
            return View("~/Views/Services/Services.cshtml");
        }

        public IActionResult EditService(ServiceDTO service)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var id = service.Id;
            var apiUrl = $"Services/{id}?parameter=0";
            var services = baseApi.PutToApi(apiUrl, service, token);
            return View("~/Views/Services/Services.cshtml");
        }
    }
}
