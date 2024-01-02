using Application.Services.Education;
using Application.Services.TopServices;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThingIdoController : AdminbaseController
    {
        private IThingIdoServices _thingIdoServices;
        public ThingIdoController(IThingIdoServices thingIdoServices)
        {
            _thingIdoServices = thingIdoServices;
        }
        public IActionResult Index()
        {
            var res = _thingIdoServices.GetAllThingIdoForAdmin();

            return View(res.Data);
        }

        [HttpGet]
        public IActionResult SubmitThingIdoFormModal(int id)
        {
            var res = _thingIdoServices.Fill(id);

            return View(model: res.Data);
        }

        [HttpPost]
        public IActionResult SubmitThingIdoFormModal(AddOrEditThingIdoDto thingIdoDto, IFormFile icon)
        {
            var _res = _thingIdoServices.AddOrEditThingIdo(new AddOrEditThingIdoDto()
            {
                Description = thingIdoDto.Description,
                Title = thingIdoDto.Title,
                Id = thingIdoDto.Id,
                Icon = icon,
                Count = thingIdoDto.Count,
            });

            return Redirect("/admin/ThingIdo/index");
        }

        public IActionResult DeleteThingIdo(int id)
        {
            var res = _thingIdoServices.DeleteThingIdo(id);

            return Json(res);
        }
    }
}
