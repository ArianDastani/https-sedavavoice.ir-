using Application.Services.Pricing;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Controllers
{
    public class PricingController : Controller
    {
        private IPricingServices _pricingServices;

        public PricingController(IPricingServices pricingServices)
        {
            _pricingServices = pricingServices;
        }
        public IActionResult Index()
        {
            var res = _pricingServices.GetAllPricingForّIndex();
            return View(res.Data);
        }
    }
}
