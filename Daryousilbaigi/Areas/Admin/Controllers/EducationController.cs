using Application.Services.Education;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : AdminbaseController
    {
        private IEducationService _educationService;
        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }
        public IActionResult Index()
        {
            var res = _educationService.GetAllEducationForAdmin();

            return View(res.Data);
        }

        public IActionResult LoadEducationFromModal(long Id)
        {
            var res = _educationService.FillEducation(Id);

            return PartialView(viewName: "_AddOrEditEducationPartial", model:res.Data);
        }


        public IActionResult SubmitEducationFormModal(AddOrEditEducationDto education)
        {
            var _res = _educationService.AddOrEditEducation(education);

            return Json(_res);
        }

        public IActionResult DeleteEducation(long Id)
        {
            var resD=_educationService.DeleteEducation(Id);

            return Json(resD);
        }
    }
}
