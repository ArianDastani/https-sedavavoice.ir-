using Application.Interface.Context;
using Common.ResultDto;
using Microsoft.AspNetCore.Http;
using Common.SaveFile;

namespace Application.Services.TopServices
{
    public interface IThingIdoServices
    {
        ResultDto DeleteThingIdo(long Id);

        ResultDto<List<ResultGetAllThingIdoForAdminDto>> GetAllThingIdoForAdmin();

        ResultDto AddOrEditThingIdo(AddOrEditThingIdoDto portfolioDto);

        ResultDto<List<ResultGetAllThingIdoForAdminDto>> GetAllThingIdoForIndex();

        ResultDto<AddOrEditThingIdoDto> Fill(int id);
    }

    public class ThingIdoServices : IThingIdoServices
    {
        private IDataBaseContext _context;
        public ThingIdoServices(IDataBaseContext context)
        {
            _context= context;
        }

        public ResultDto AddOrEditThingIdo(AddOrEditThingIdoDto ThingDto)
        {
            string imagename = "";
            if (ThingDto.Id == 0)
            {

                if (ThingDto.Icon != null)
                {
                    if (Path.GetExtension(ThingDto.Icon.FileName) == ".png" || Path.GetExtension(ThingDto.Icon.FileName) == ".jpeg" || Path.GetExtension(ThingDto.Icon.FileName) == ".jpg")
                    {
                        imagename = Guid.NewGuid().ToString("N") + Path.GetExtension(ThingDto.Icon.FileName);
                        ThingDto.Icon.AddImageAjaxToServer(imagename, Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/content/thingIdo/Image/"));

                    }

                }

                Domain.Models.Service service = new Domain.Models.Service
                {
                    Description = ThingDto.Description,
                    Count = ThingDto.Count,
                    Icon = imagename,
                    Title = ThingDto.Title,
                    
                    
                };

                _context.Services.Add(service);
                _context.SaveChanges();

                return new ResultDto { IsSuccess = true, Message = "با موفقیت اضافه شد" };
            }
            else
            {
                
                var thng=_context.Services.FirstOrDefault(x=>x.Id== ThingDto.Id);
                if(thng == null) { return new ResultDto { IsSuccess = false, Message = "یافت نشد" }; }

                imagename = thng.Icon;

                if (ThingDto.Icon != null)
                {
                    if (Path.GetExtension(ThingDto.Icon.FileName) == ".png" || Path.GetExtension(ThingDto.Icon.FileName) == ".jpeg" || Path.GetExtension(ThingDto.Icon.FileName) == ".jpg")
                    {
                        imagename = Guid.NewGuid().ToString("N") + Path.GetExtension(ThingDto.Icon.FileName);
                        ThingDto.Icon.AddImageAjaxToServer(imagename, Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/content/thingIdo/Image/"));

                    }

                }

                thng.Title = ThingDto.Title;
                thng.Description = ThingDto.Description;
                thng.Icon = imagename;
                thng.Count = ThingDto.Count;
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = "با موفقیت ویرایش شد" };

            }

        }

        public ResultDto DeleteThingIdo(long Id)
        {
            var thing=_context.Services.FirstOrDefault(x=>x.Id== Id);
            if (thing == null) { return new ResultDto { IsSuccess = false, Message = "یافت نشد" }; }

            thing.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto { Message = "با موفقیت حذف شد", IsSuccess = true };
        }

        public ResultDto<AddOrEditThingIdoDto> Fill(int id)
        {
            if (id == 0 || id == null)
            {
                return new ResultDto<AddOrEditThingIdoDto> { Data = new AddOrEditThingIdoDto { Id = 0 }, IsSuccess = false };
            }

            var res=_context.Services.FirstOrDefault(x=>x.Id == id);

            if (res == null)
            {
                return new ResultDto<AddOrEditThingIdoDto> { Data = new AddOrEditThingIdoDto { Id = 0 }, IsSuccess = false };

            }

            return new ResultDto<AddOrEditThingIdoDto>
            {
                Data=new AddOrEditThingIdoDto
                {
                    Count=res.Count,
                    Description=res.Description,
                    Title=res.Title,
                    Id=id
                },
                IsSuccess=true
                ,Message=""
            };
        }

        public ResultDto<List<ResultGetAllThingIdoForAdminDto>> GetAllThingIdoForAdmin()
        {
            var thingIdo = _context.Services
                .Where(x => x.IsRemoved == false)
                .Select(_ => new ResultGetAllThingIdoForAdminDto
                {
                    Id=_.Id,
                    Description=_.Description,
                    Count=_.Count,
                    Icon=_.Icon,
                    Title=_.Title,
                }).ToList();

            return new ResultDto<List<ResultGetAllThingIdoForAdminDto>>
            {
                Data = thingIdo,
                IsSuccess = true,
                Message=""
                
            };

        }

        public ResultDto<List<ResultGetAllThingIdoForAdminDto>> GetAllThingIdoForIndex()
        {
            var thingIdo = _context.Services
           .Where(x => x.IsRemoved == false)
           .Select(_ => new ResultGetAllThingIdoForAdminDto
           {
               Description = _.Description,
               Count = _.Count,
               Icon = _.Icon,
               Title = _.Title,
           }).ToList();

            return new ResultDto<List<ResultGetAllThingIdoForAdminDto>>
            {
                Data = thingIdo,
                IsSuccess = true,
                Message = ""

            };
        }
    }

    public class ResultGetAllThingIdoForAdminDto
    {
        public int? Id { get; set; }

        public string? Icon { get; set; } 

        public string? Title { get; set; } 

        public string? Description { get; set; } 

        public string? Count { get; set; } 
    }

    public class AddOrEditThingIdoDto
    {
        public int Id { get; set; }

        public IFormFile? Icon { get; set; } 

        public string? Title { get; set; }

        public string? Description { get; set; } 

        public string? Count { get; set; } 
    }
}
