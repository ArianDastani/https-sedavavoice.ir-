using Application.Interface.Context;
using Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Message.Command.RemoveMessage
{
    public interface IRemoveMessage
    {
        ResultDto Execute(int id);
    }

    public class RemoveMessage : IRemoveMessage
    {
        private IDataBaseContext _context;
        public RemoveMessage(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(int id)
        {
            var message = _context.Messages.Find(id);
            message.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "پیام باموفقیت حدف شد"
            };

        }
    }
}
