using Application.Services.Information;
using Microsoft.AspNetCore.Mvc;

namespace Store.ViewComponnent.Search
{
    public class Search : ViewComponent
    {
        private IInformationServices _informationServices;

        public Search(IInformationServices informationServices)
        {
            _informationServices = informationServices;
        }
        public IViewComponentResult Invoke()
        {

            var res=_informationServices.GetInfo();
            return View(viewName: "Components/Search/Search.cshtml",res.Data);
        }
    }
}
