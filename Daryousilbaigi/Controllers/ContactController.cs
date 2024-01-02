using Application.Interface.Context;
using Application.Services.Information;
using Application.Services.Message.Command.SendMessage;
using Daryousilbaigi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Controllers
{
    public class ContactController : Controller
    {
        private ISendMessageService _sendService;
        private IInformationServices _informationServices;
        public ContactController(ISendMessageService sendMessageService, IInformationServices informationServices)
        {
            _sendService = sendMessageService;
            _informationServices = informationServices;
        }
        public IActionResult Index()
        {
            var res = _informationServices.GetInfo();


            ContactViewModel contactViewModel = new ContactViewModel()
            {
                GetInfo = res.Data
            };

            return View(contactViewModel);
        }

        [HttpPost]
        public IActionResult SendMessage(string name,string number,string text)
        {
            

            var result = _sendService.Excute(new SendMessageDto
            {
                Name = name,
                PhoneNumber = number,
                Text = text
            });


            return Json(result);
        }
    }
}
