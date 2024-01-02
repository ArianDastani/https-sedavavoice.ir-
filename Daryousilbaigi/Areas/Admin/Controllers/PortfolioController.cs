using Application.Interface.Context;
using Application.Services.Portfolio;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : AdminbaseController
    {
        private IPortfolioServices _portfolioServices;
        public PortfolioController(IPortfolioServices portfolioServices)
        {
            _portfolioServices = portfolioServices;
        }
        public IActionResult Index()
        {
            var res = _portfolioServices.GetAllPortfolioForAdmin();

            return View(res.Data);
        }

        [HttpGet]
        public IActionResult LoadAddOrEditPortFolio(long Id)
        {
            var res = _portfolioServices.FillPortFolio(Id);

            return View(model: res.Data);
        }

        //public IActionResult LoadPortfolioFromModal(long Id)
        //{
        //    var res=_portfolioServices.FillPortFolio(Id);

        //    return PartialView(viewName: "_AddorEditPortfolioPartial", model: res.Data);
        //}

        [HttpPost, ValidateAntiForgeryToken]
        [RequestSizeLimit(500 * 1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 500 * 1024 * 1024)]
        public IActionResult SubmitPortfolioFromModal(AddOrEditPortfolioDto portfolioDto,IFormFile Image2,IFormFile Video2)
        {
            var _res = _portfolioServices.AddOrEditPortfolio(portfolioDto,Image2,Video2);

            return RedirectToAction(controllerName: "Portfolio",actionName:"Index");
        }

        public IActionResult DeletePortfolio(long id)
        {
            var resD = _portfolioServices.DeletePortfolio(id);

            return Json(resD);
        }
    }
}
