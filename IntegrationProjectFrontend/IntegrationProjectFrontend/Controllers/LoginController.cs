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
using System.IdentityModel.Tokens.Jwt;

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
            if (responseLogin != null && responseLogin.Value != null)
            {
               var responseObject = JsonConvert.DeserializeObject<ApiResponse<UserLogin>>(responseLogin.Value.ToString());
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                // Claim claimName = new(ClaimTypes.Name, responseObject.Data.FirstName);

                JwtSecurityTokenHandler hand = new JwtSecurityTokenHandler();
                var tokenData = hand.ReadJwtToken(responseObject.Data.Token);

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(tokenData.Claims);

                Claim claimRole;

                var roleClaim = claimsIdentity.FindFirst(claim => claim.Type == ClaimTypes.Role);

                if (roleClaim != null && roleClaim.Value == "1")
                {
                    claimRole = new Claim(ClaimTypes.Role, "Administrador");
                }
                else
                {
                    claimRole = new Claim(ClaimTypes.Role, "Consultor");
                }

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
            else
            {
                var loginViewModel = new LoginViewModel();
                loginViewModel.ErrorMessage = "Invalid username or password."; // Customize this message as needed
                return View("~/Views/Login/Login.cshtml", loginViewModel);
            }
            

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
