using Data.Base;
using Data.DTOs;
using IntegrationProjectFrontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace IntegrationProjectFrontend.Controllers
{

    [Authorize]
    public class UsersController : Controller
    {

        private readonly IHttpClientFactory _httpClient;
        public UsersController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Users()
        {
            return View();
        }

        public async Task<IActionResult> UsersAddPartial([FromBody] UserDTO userDTO)
        {
             var usersViewModel = new UsersViewModel();

             if (userDTO != null)
             {
                 usersViewModel = userDTO;
             }

            return PartialView("~/Views/Users/Partial/UsersAddPartial.cshtml", usersViewModel);
        }
        public IActionResult RegisterUser(UserDTO user)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var users = baseApi.PostToApi("Users", user, token);
            return View("~/Views/Users/Users.cshtml");
        }

        public IActionResult EditUser(UserDTO user)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var users = baseApi.PutToApi("Users", user, token);
            return View("~/Views/Users/Users.cshtml");
        }
    }
}