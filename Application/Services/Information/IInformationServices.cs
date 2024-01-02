

using System.ComponentModel.DataAnnotations;
using Application.Interface.Context;
using Common.ResultDto;
using Common.SaveFile;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Information
{
    public interface IInformationServices
    {
        ResultDto<GetInfoDto> GetInfo();
        ResultDto AddViewCount();
        ResultDto UpdateInfo(RequestUpdateInfoDto UpdateInfoDto);

        ResultDto<GetInformationsForAdmin> GetInformationsForAdmin();
    }

    public class InformationServices : IInformationServices
    {
        private IDataBaseContext _context;
        public InformationServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddViewCount()
        {
            var res = _context.Informations.FirstOrDefault(x => x.Id == 1);

            var count = int.Parse(res.ViewCount);

            count++;

            res.ViewCount = count.ToString();
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };

        }

        public ResultDto<GetInfoDto> GetInfo()
        {
            var info = _context.Informations
                .Select(x => new GetInfoDto
                {
                    Address = x.Address,
                    Experience = x.Experience,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    Instagram = x.Instagram,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    ProfileImage = x.ProfileImage,
                    TelegramChannel = x.TelegramChannel,
                    ViewCount = x.ViewCount,
                    AboutMe = x.AboutMe
                })
                .FirstOrDefault(y => y.Id == 1);

            return new ResultDto<GetInfoDto>
            {
                Data = info,
                IsSuccess = true
                ,
                Message = ""
            };
        }

        public ResultDto UpdateInfo(RequestUpdateInfoDto UpdateInfoDto)
        {
            var res = _context.Informations.FirstOrDefault(x => x.Id == 1);

            string imageName = res.ProfileImage;

            if (UpdateInfoDto.ProfileImage != null)
            {
                imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(UpdateInfoDto.ProfileImage.FileName);
                UpdateInfoDto.ProfileImage.AddImageAjaxToServer(imageName, $"{Directory.GetCurrentDirectory()}/wwwroot/content/profileimage/");
            }

            res.Address = UpdateInfoDto.Address;
            res.PhoneNumber = UpdateInfoDto.PhoneNumber;
            res.LastName = UpdateInfoDto.LastName;
            res.FirstName = UpdateInfoDto.FirstName;
            res.Experience = UpdateInfoDto.Experience;
            res.Instagram = UpdateInfoDto.Instagram;
            res.ProfileImage = imageName;
            res.TelegramChannel = UpdateInfoDto.TelegramChannel;
            res.AboutMe = UpdateInfoDto.AboutMe;

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "با موفقیت ویرایش شد"
            };
        }

        public ResultDto<GetInformationsForAdmin> GetInformationsForAdmin()
        {
            var message = _context.Messages.Count().ToString();
            var viewcount = _context.Informations.FirstOrDefault(x => x.Id == 1).ViewCount;
            var portfolio = _context.Portfolios.Count().ToString();
            var allviewPortfolio = _context.Portfolios
                .Select(x => x.ViewCount).Sum().ToString();

            return new ResultDto<GetInformationsForAdmin>()
            {
                Data = new GetInformationsForAdmin() { TotalMessage = message, TotalPortfolio = portfolio, TotalVisits = viewcount, VisitsAllPortfolio = allviewPortfolio }
                ,Message = "",
                IsSuccess = true
            };
        }
    }

    public class GetInfoDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TelegramChannel { get; set; }
        public string? Instagram { get; set; }
        public string? Address { get; set; }
        public string? Experience { get; set; }
        public string? ViewCount { get; set; }
        public string? ProfileImage { get; set; }
        public string? AboutMe { get; set; }

    }

    public class RequestUpdateInfoDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TelegramChannel { get; set; }
        public string? Instagram { get; set; }
        public string? Address { get; set; }
        public string? Experience { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? AboutMe { get; set; }

    }

    public class GetInformationsForAdmin
    {
        public string? TotalVisits { get; set; }
        public string? TotalMessage { get; set; }
        public string? TotalPortfolio { get; set; }
        public string? VisitsAllPortfolio { get; set; }
    }
}
