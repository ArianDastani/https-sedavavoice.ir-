using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Services.User;

namespace Daryousilbaigi.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUserServices _userServices;

        public AuthenticationController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string pass)
        {
            var res = _userServices.LoginUser(username, pass);

            if (res.IsSuccess == true)
            {
                var claims = new List<Claim>()
                {

                    new Claim(ClaimTypes.NameIdentifier,res.Data.UserName.ToString()),
                    new Claim("UserId",res.Data.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var pricipal = new ClaimsPrincipal(identity);
                var propertis = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5),
                };

                HttpContext.SignInAsync(pricipal, propertis);
            }

            return Json(res);
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");

        }
    }
}
