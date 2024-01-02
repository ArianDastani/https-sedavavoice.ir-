using Application.Services.Information;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.ViewComponent
{
    public class SideBarViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private IInformationServices _informationServices;
        public SideBarViewComponent(IInformationServices informationServices)
        {
            _informationServices = informationServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = _informationServices.GetInfo();

            return View(viewName: "Components/SideBarViewComponent.cshtml", model:res.Data);
        }
    }
}
