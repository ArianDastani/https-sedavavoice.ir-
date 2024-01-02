using Application.Services.Skills;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillsController : AdminbaseController
    {
        private ISkillServices _skillServices;
        public SkillsController(ISkillServices skillServices)
        {
            _skillServices = skillServices;
        }
        public IActionResult Index()
        {
            var res=_skillServices.GetAllSkillsForAdmin();

            return View(res.Data);
        }

        public IActionResult LoadAddSkillsFormModal(long id)
        {
            var skill = _skillServices.FillSkill(id);

            return PartialView(viewName: "_AddOrEditSkillsPartial", model:skill.Data);

        }

        public IActionResult SubmitSkillFormModal(AddOrEditSkillsDto skillsDto)
        {
            var res = _skillServices.AddOrEditSkill(skillsDto);

            if (res.IsSuccess == true)
            {
                return Json(new { Status = "Success" });
            }

            return new JsonResult(new { status = "Error" });
        }

        public IActionResult DeleteSkill(long id)
        {
            var resD = _skillServices.DeleteSkill(id);

            return Json(resD);
        }
    }
}
