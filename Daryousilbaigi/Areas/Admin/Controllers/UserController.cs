using Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : AdminbaseController
    {
        private IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public IActionResult Index()
        {
            var res = _userServices.GetUser();

            return View(res.Data);
        }

        [HttpPost]
        public IActionResult Index(string UserName,string Password)
        {
            var res = _userServices.EditUser(UserName,Password);

            return Redirect("/admin");
        }
    }
}
