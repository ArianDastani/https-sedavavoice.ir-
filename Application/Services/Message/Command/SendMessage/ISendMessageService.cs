using Application.Interface.Context;
using Common.ResultDto;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Message.Command.SendMessage
{
    public interface ISendMessageService
    {
        ResultDto Excute(SendMessageDto messageDto);
    }

    public class SendMessageService : ISendMessageService
    {
        private IDataBaseContext _context;
        public SendMessageService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Excute(SendMessageDto messageDto)
        {

            var m=_context.Messages.ToList();

            if (string.IsNullOrWhiteSpace(messageDto.Text) ||
                string.IsNullOrWhiteSpace(messageDto.Name) ||
                string.IsNullOrWhiteSpace(messageDto.PhoneNumber))
            {
                return new ResultDto { IsSuccess = false, Message = "نام خود و شماره تماس و پیام خود را وارد کنید" };

            }

            Domain.Models.Message message = new Domain.Models.Message
            {
                Name = messageDto.Name,
                PhoneNumber = messageDto.PhoneNumber,
                Text= messageDto.Text,
                
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return new ResultDto { IsSuccess = true, Message = "ارسال پیام با موفقیت انجام شد" };



        }
    }

    public class SendMessageDto
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Text { get; set; } = null!;

    }
}
