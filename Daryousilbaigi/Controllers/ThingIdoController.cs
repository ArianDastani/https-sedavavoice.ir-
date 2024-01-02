using Application.Services.TopServices;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Controllers
{
    public class ThingIdoController : Controller
    {
        private IThingIdoServices _thingIdoServices;

        public ThingIdoController(IThingIdoServices thingIdoServices)
        {
            _thingIdoServices = thingIdoServices;
        }
        public IActionResult Index()
        {
            var res = _thingIdoServices.GetAllThingIdoForIndex().Data;

            return View(res);
        }
    }
}
