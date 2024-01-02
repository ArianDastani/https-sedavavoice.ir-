using Application.Interface.Context;
using Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Message.Query
{
    public interface IGetMessage
    {
        ResultDto<List<GetMessageDto>> Excute(bool? All,bool? IsRemove, bool? Stared, bool? IsRead);
    }

    public class GetMessage : IGetMessage
    {
        private IDataBaseContext _context;
        public GetMessage(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetMessageDto>> Excute(bool? All,bool? IsRemove, bool? Stared, bool? IsRead)
        {
            var Messageuery = _context.Messages.ToList().AsQueryable();

            if (IsRemove == true)
            {
                All = false;
                Messageuery = Messageuery.Where(x => x.IsRemoved == true).AsQueryable();
            }

            if (Stared == true)
            {
                All = false;
                Messageuery = Messageuery.Where(x => x.IsRemoved == false || x.Starred == true).AsQueryable();

            }

            if (IsRead == true)
            {
                All = false;
                Messageuery = Messageuery.Where(x => x.IsRemoved == false &&x.IsRead==false).AsQueryable();

            }

            if (All == true)
            {
                Messageuery = Messageuery.Where(x => x.IsRemoved == false).AsQueryable();

            }



            return new ResultDto<List<GetMessageDto>>
            {
                Data = Messageuery.Select(x => new GetMessageDto
                {
                    IsRead = x.IsRead,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Text = x.Text,
                    Starred = x.Starred,
                    IsRemoved = x.IsRemoved,
                    Id = x.Id
                }).ToList(),
                Message = "",
                IsSuccess = true
            };
        }
    }

    public class GetMessageDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Text { get; set; } = null!;

        public bool IsRemoved { get; set; }

        public bool IsRead { get; set; }
        public bool Starred { get; set; }
    }
}
