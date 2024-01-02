using Application.Services.Information;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InformationsController : AdminbaseController
    {
        private IInformationServices _informationServices;
        public InformationsController(IInformationServices informationServices)
        {
            _informationServices = informationServices;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var re2s = _informationServices.GetInfo();

            return View(re2s.Data);
        }

        [HttpPost]
        public IActionResult Edit(GetInfoDto infoDto,IFormFile image)
        {
            var re2s = _informationServices.UpdateInfo(new RequestUpdateInfoDto
            {
                AboutMe = infoDto.AboutMe,
                Address = infoDto.Address,
                Experience = infoDto.Experience,
                FirstName = infoDto.FirstName,
                Instagram = infoDto.Instagram,
                Id = infoDto.Id,
                LastName = infoDto.LastName,
                PhoneNumber = infoDto.PhoneNumber,
                ProfileImage = image,
                TelegramChannel = infoDto.TelegramChannel,
            });

            return Redirect("/admin/Informations/Edit");
        }
    }
}
