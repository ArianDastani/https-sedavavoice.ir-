using Application.Interface.Context;
using Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Message.Query
{
    public interface IReadMessage
    {
        ResultDto<ReadMessageDto> Execute(int id);
    }

    public class ReadMessage: IReadMessage
    {
        private IDataBaseContext _context;
        public ReadMessage(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ReadMessageDto> Execute(int id)
        {
            var message = _context.Messages.Find(id);

            if (message.IsRead == false)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return new ResultDto<ReadMessageDto>
            {
                Data = new ReadMessageDto
                {
                    Id= id,
                    Name = message.Name,
                    PhoneNumber = message.PhoneNumber,
                    Text = message.Text,
                },
                Message = "",
                IsSuccess = true
            };
        }
    }

    public class ReadMessageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Text { get; set; } = null!;

    }
}
