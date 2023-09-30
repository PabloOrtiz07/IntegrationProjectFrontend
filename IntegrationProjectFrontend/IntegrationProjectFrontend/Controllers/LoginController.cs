using Data.DTOs;
using Data.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using IntegrationProjectFrontend.Models;
using IntegrationProjectFrontend.ViewModels;

namespace IntegrationProjectFrontend.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public LoginController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginByEmail(LoginDto login)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Authorize", login);
            var responseLogin = token as OkObjectResult;

            var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserLogin>>(responseLogin.Value.ToString());

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            // Claim claimName = new(ClaimTypes.Name, responseObject.Data.FirstName);
            Claim claimRole = new(ClaimTypes.Role, "Administrator");


           // identity.AddClaim(claimName);
            identity.AddClaim(claimRole);

            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddHours(1),
            });

             HttpContext.Session.SetString("Token", responseObject.Data.Token);

             var homeViewModel = new HomeViewModel();
             homeViewModel.Token = responseObject.Data.Token;

            return View("~/Views/Home/Index.cshtml", homeViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
