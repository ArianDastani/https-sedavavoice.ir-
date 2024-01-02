using Application.Services.Education;
using Application.Services.Information;
using Application.Services.Portfolio;
using Application.Services.Skills;
using Daryousilbaigi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Daryousilbaigi.Controllers
{
    public class HomeController : Controller
    {
        private IInformationServices _informationServices;
        private ISkillServices _skillServices;
        private IEducationService _educationService;
        private IPortfolioServices _portfolioServices;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IInformationServices informationServices, ISkillServices skillServices , IEducationService educationService,IPortfolioServices portfolioServices)
        {
            _logger = logger;
            _educationService = educationService;
            _skillServices = skillServices;
            _informationServices = informationServices;
            _portfolioServices = portfolioServices;
        }

        public IActionResult Index()
        {
            var res = _informationServices.GetInfo();
            var skills = _skillServices.GetSkillsForIndex();
            var edu=_educationService.GetAllEducationForIndex();
            var portfilio = _portfolioServices.GetAllportfolioForIndex();

            var cunt= _informationServices.AddViewCount();

            IndexViewModel indexViewModel = new IndexViewModel
            {
                GetInfo = res.Data,
                EducationForindex=edu.Data,
                SkillsForIndex = skills.Data,
                GetAllPortfolioForIndex = portfilio.Data
            };

            return View(indexViewModel);
        }



        [Route("/service")]
        public IActionResult service()
        {
            return View();
        }


        //[Route("/pricing")]
        //public IActionResult pricing()
        //{
        //    return View();
        //}

        //public IActionResult skill()
        //{
        //    return View();
        //}


        //[Route("/portfolio")]
        //public IActionResult portfolio()
        //{
        //    return View();
        //}


        [Route("/blog")]
        public IActionResult blog()
        {
            return View();
        }


        [Route("/testimonials"),ValidateAntiForgeryToken()]
        public IActionResult testimonials()
        {
            return View();
        }


        public IActionResult Detail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}