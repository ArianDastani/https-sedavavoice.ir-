using Application.Interface.Context;
using Common.ResultDto;
using Common.SaveFile;
using Microsoft.AspNetCore.Http;


namespace Application.Services.Portfolio
{
    public interface IPortfolioServices
    {
        ResultDto DeletePortfolio(long Id);

        ResultDto<ResultGetDetailPortfolioForIndexDto> GetDetailPortfolioForIndex(long Id);

        ResultDto<List<ResultGetAllPortfolioForAdminDto>> GetAllPortfolioForAdmin();

        ResultDto AddOrEditPortfolio(AddOrEditPortfolioDto portfolioDto,IFormFile Image,IFormFile Video);

        ResultDto<AddOrEditPortfolioDto> FillPortFolio(long Id);

        ResultDto<List<GetAllportfolioForIndexDto>> GetAllportfolioForIndex();
        ResultDto AddViewCounter(int id);
        ResultDto<List<GetAllportfolioForIndexDto>> GetAllportfolio();
    }

    public class PortfolioServices : IPortfolioServices
    {
        private IDataBaseContext _dbContext;
        public PortfolioServices(IDataBaseContext context)
        {
            _dbContext = context;
        }

        public ResultDto AddOrEditPortfolio(AddOrEditPortfolioDto portfolioDto, IFormFile Image, IFormFile Video)
        {
            string imageName = "";
            string videoName = "";

            if (portfolioDto.Id == 0)
            {
              

                if (Image != null)
                {
                    if (Path.GetExtension(Image.FileName)==".png"|| Path.GetExtension(Image.FileName) == ".jpeg"|| Path.GetExtension(Image.FileName) == ".jpg")
                    {
                        imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(Image.FileName);
                        Image.AddImageAjaxToServer(imageName, Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/content/Portfolio/Image/"));

                    }

                }

                if (Video != null)
                {
                    if (Path.GetExtension(Video.FileName) == ".mp4")
                    {
                        videoName = Guid.NewGuid().ToString("N") + Path.GetExtension(Video.FileName);
                        Video.AddImageAjaxToServer(videoName, Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/content/Portfolio/video/"));

                    }

                }

                Domain.Models.Portfolio portfolio = new Domain.Models.Portfolio
                {
                    CreateDate = DateTime.Now,
                    Description = portfolioDto.Description,
                    ImageAlt = portfolioDto.ImageAlt,
                    Link = portfolioDto.Link,
                    Title = portfolioDto.Title,
                    ImageTitle = imageName,
                    Video = videoName,

                };

                _dbContext.Portfolios.Add(portfolio);
                _dbContext.SaveChanges();


                return new ResultDto { IsSuccess = true, Message = "با موفقیت اضافه شد" };
            }
            else
            {
                var port = _dbContext.Portfolios.FirstOrDefault(x => x.Id == portfolioDto.Id);

                if (port == null) { return new ResultDto { IsSuccess = false, Message = "یافت نشد" }; }

                imageName = port.ImageTitle;
                videoName = port.Video;

                if (Image != null)
                {
                    if (Path.GetExtension(Image.FileName) == ".png" || Path.GetExtension(Image.FileName) == ".jpeg" || Path.GetExtension(Image.FileName) == ".jpg")
                    {
                        imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(Image.FileName);
                        Image.AddImageAjaxToServer(imageName, Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/content/Portfolio/Image/"));

                    }

                }

                if (Video != null)
                {
                    if (Path.GetExtension(Video.FileName) == ".mp4")
                    {
                        videoName = Guid.NewGuid().ToString("N") + Path.GetExtension(Video.FileName);
                        Video.AddImageAjaxToServer(videoName, Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/content/Portfolio/video/"));

                    }

                }

                port.Video = videoName;
                port.Description = portfolioDto.Description;
                port.CreateDate = portfolioDto.CreateDate;
                port.ImageAlt = portfolioDto.ImageAlt;
                port.Link = portfolioDto.Link;
                port.Title = portfolioDto.Title;
                port.ImageTitle = imageName;

                _dbContext.SaveChanges();

                return new ResultDto { IsSuccess = true, Message = "با موفقیت ویرایش شد" };

            }

        }

        public ResultDto AddViewCounter(int id)
        {
            if (id != null || id != 0)
            {
                var res = _dbContext.Portfolios.FirstOrDefault(x => x.Id == id);
                res.ViewCount++;
                _dbContext.SaveChanges();

                return new ResultDto
                {
                    IsSuccess= true,
                    Message=""
                };
            }

            return new ResultDto
            {
                IsSuccess = false,
                Message = ""
            };


        }

        public ResultDto DeletePortfolio(long Id)
        {
            var port = _dbContext.Portfolios.FirstOrDefault(p => p.Id == Id);
            if (port == null) { return new ResultDto { IsSuccess = false, Message = "یافت نشد" }; }

            port.IsRemoved = true;
            _dbContext.SaveChanges();

            return new ResultDto { IsSuccess = true, Message = "با موفقیت حذف شد" };
        }

        public ResultDto<AddOrEditPortfolioDto> FillPortFolio(long Id)
        {
            if (Id == 0)
            {
                return new ResultDto<AddOrEditPortfolioDto>
                {
                    Data = new AddOrEditPortfolioDto { Id = 0 },
                };
            }

            var res = _dbContext.Portfolios.FirstOrDefault(i => i.Id == Id);

            if (res == null)
            {
                return new ResultDto<AddOrEditPortfolioDto>
                {
                    Data = new AddOrEditPortfolioDto { Id = 0 },
                };
            }



            return new ResultDto<AddOrEditPortfolioDto>
            {
                Data = new AddOrEditPortfolioDto
                {
                    Id=res.Id,
                    Description=res.Description,
                    Image = res.ImageTitle,
                    ImageAlt =res.ImageAlt,
                    Link=res.Link,
                    Title=res.Title,
                    Video=res.Video,
                    

                }
            };
        }

        public ResultDto<List<ResultGetAllPortfolioForAdminDto>> GetAllPortfolioForAdmin()
        {
            var portfolois = _dbContext.Portfolios
                .Where(x => x.IsRemoved == false)
                .Select(y => new ResultGetAllPortfolioForAdminDto
                {
                    Id = y.Id,
                    CreateDate = y.CreateDate,
                    ImageAlt = y.ImageAlt,
                    Image=y.ImageTitle,
                    Link = y.Link,
                    Title = y.Title,
                    ViewCount = y.ViewCount,
                })
                .ToList();

            return new ResultDto<List<ResultGetAllPortfolioForAdminDto>>
            {
                Data = portfolois,
                IsSuccess = true,
                Message = ""

            };
        }

        public ResultDto<List<GetAllportfolioForIndexDto>> GetAllportfolioForIndex()
        {
            return new ResultDto<List<GetAllportfolioForIndexDto>>
            {
                Data = _dbContext.Portfolios
                .Where(x => x.IsRemoved == false)
                .Select(x=>new GetAllportfolioForIndexDto
                {
                    Id=x.Id,
                    ImageAlt = x.ImageAlt,
                    Image = x.ImageTitle,
                    Title = x.Title,
                })
                .Take(4)
                .ToList(),
            };

            
        }

        public ResultDto<List<GetAllportfolioForIndexDto>> GetAllportfolio()
        {
            return new ResultDto<List<GetAllportfolioForIndexDto>>
            {
                Data = _dbContext.Portfolios
                .Where(x => x.IsRemoved == false)
                .Select(x => new GetAllportfolioForIndexDto
                {
                    Id = x.Id,
                    ImageAlt = x.ImageAlt,
                    Image = x.ImageTitle,
                    Title = x.Title,
                })
                .ToList(),
            };


        }


        public ResultDto<ResultGetDetailPortfolioForIndexDto> GetDetailPortfolioForIndex(long Id)
        {
            var port = _dbContext.Portfolios
                .Select(y => new ResultGetDetailPortfolioForIndexDto
                {
                    Description = y.Description,
                    CreateDate = y.CreateDate,
                    Id = y.Id,
                    Image = y.ImageTitle,
                    ImageAlt = y.ImageAlt,
                    Link = y.Link,
                    Title = y.Title,
                    Video = y.Video,
                    ViewCount = y.ViewCount,
                })
                .FirstOrDefault(x => x.Id == Id);



            if (port == null)
            {
                return new ResultDto<ResultGetDetailPortfolioForIndexDto>
                {
                    Message = "یافت نشد",
                    IsSuccess = false
                };
            }

            return new ResultDto<ResultGetDetailPortfolioForIndexDto>
            {
                Data = port,
                IsSuccess = true,
                Message = ""
            };


        }
    }

    public class ResultGetDetailPortfolioForIndexDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string Link { get; set; } = null!;

        public string ImageAlt { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public int ViewCount { get; set; } = 0;

        public string? Video { get; set; } 
    }

    public class ResultGetAllPortfolioForAdminDto
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Image { get; set; }

        public string? Link { get; set; }

        public string? ImageAlt { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? ViewCount { get; set; } 

    }

    public class AddOrEditPortfolioDto
    {
        public int? Id { get; set; }

        public string Title { get; set; } = null!;

        public string Link { get; set; } = null!;

        public string? Image { get; set; }
        public string ImageAlt { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreateDate { get; set; }

        public string? Video { get; set; } 
    }

    public class GetAllportfolioForIndexDto
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Image { get; set; }

        public string? ImageAlt { get; set; }
    }

}
