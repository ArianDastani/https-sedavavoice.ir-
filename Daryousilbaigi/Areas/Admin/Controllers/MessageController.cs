using Application.Services.Message.Command.RemoveMessage;
using Application.Services.Message.Query;
using Microsoft.AspNetCore.Mvc;

namespace Daryousilbaigi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : AdminbaseController
    {
        private IGetMessage _getmessage;
        private IReadMessage _readmessage;
        private IRemoveMessage _removemessage;
        public MessageController(IGetMessage getMessage, IReadMessage readmessage,IRemoveMessage removeMessage)
        {
            _getmessage = getMessage;
            _readmessage = readmessage;
            _removemessage = removeMessage;

        }
        public IActionResult Index(bool? All, bool? IsRemove, bool? Stared, bool? IsRead)
        {
            var result = _getmessage.Excute(All = true, IsRemove, Stared, IsRead);

            return View(result.Data);
        }

        public IActionResult Read(int id)
        {
            var result=_readmessage.Execute(id);

            return View(result.Data);
        }

        public IActionResult Delete(int id)
        {
            var r = _removemessage.Execute(id);

            return Json(r);
        }
    }
}
