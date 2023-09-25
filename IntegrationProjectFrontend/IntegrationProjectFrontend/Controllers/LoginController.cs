
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationProjectFrontend.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Ingresar(LoginDto login)
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
