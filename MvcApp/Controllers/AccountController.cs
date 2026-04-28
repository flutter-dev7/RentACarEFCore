using Infrastrucure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcApp.ViewModels;

namespace MvcApp.Controllers
{
    public class AccountController(IAuthService authService) : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // "Реальная" проверка через JWT сервис
            var token = await authService.LoginAsync(model.Username, model.Password);

            if (token == null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
                return View(model);
            }

            // Записываем токен в куки (HttpOnly для безопасности)
            Response.Cookies.Append("jwt", token, new CookieOptions 
            { 
                HttpOnly = true, 
                Secure = true, 
                Expires = DateTime.UtcNow.AddHours(3) 
            });

            return RedirectToAction("Index", "Cars");
        }
    }
}
