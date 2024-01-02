using Application.Services.Pricing;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PricingController : AdminbaseController
    {
        private IPricingServices _servicesPrice;

        public PricingController(IPricingServices servicesPrice)
        {
            _servicesPrice = servicesPrice;
        }
        public IActionResult Index()
        {
            var res = _servicesPrice.GetAllPricingForAdmin();
            return View(res.Data);
        }

        [HttpGet]
        public IActionResult SubmitPricing(long id)
        {
            var res = _servicesPrice.Fill(id).Data;

            return View(res);
        }

        [HttpPost]
        public IActionResult SubmitPricing(AddOrEditPricingDto dto)
        {
            var res = _servicesPrice.AddOrEditPricing(dto);

            return Redirect("/admin");
        }

        public IActionResult DeletePricing(long id)
        {
            var resD = _servicesPrice.DeletePricing(id);

            return Json(resD);
        }
    }
}
