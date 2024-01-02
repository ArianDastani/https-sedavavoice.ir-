using Application.Services.Portfolio;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Controllers
{
    public class PortfolioController : Controller
    {
        private IPortfolioServices _portfolioServices;
        public PortfolioController(IPortfolioServices portfolioServices)
        {
            _portfolioServices = portfolioServices;
        }

        public IActionResult Index()
        {
            var res = _portfolioServices.GetAllportfolio();

            return View(res.Data);
        }
        public IActionResult Detail(int Id)
        {
            var res=_portfolioServices.GetDetailPortfolioForIndex(Id);
            var count = _portfolioServices.AddViewCounter(Id);

            if (res.IsSuccess == false)
            {
                return Redirect("/");

            }

            return View(res.Data);
        }
    }
}
