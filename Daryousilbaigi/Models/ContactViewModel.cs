using Application.Services.Information;
using Application.Services.Message.Command.SendMessage;

namespace Daryousilbaigi.Models
{
    public class ContactViewModel
    {
        public SendMessageDto? SendMessageDto { get; set; }
        public GetInfoDto? GetInfo { get; set; }
    }
}
