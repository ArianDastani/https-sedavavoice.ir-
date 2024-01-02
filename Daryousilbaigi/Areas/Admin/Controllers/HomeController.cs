using Application.Services.Information;
using Daryousilbaigi.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AdminbaseController
    {
        private IInformationServices _Information;

        public HomeController(IInformationServices information)
        {
            _Information = information;
        }
        public IActionResult Index()
        {
            var res = _Information.GetInformationsForAdmin().Data;

            AdminIndexViewModels adminIndexView = new AdminIndexViewModels()
            {
                GetInformationsForAdmin = res
            };

            return View(adminIndexView);
        }
    }
}
