using Application.Interface.Context;
using Common.ResultDto;


namespace Application.Services.Pricing
{
    public interface IPricingServices
    {
        ResultDto<AddOrEditPricingDto> AddOrEditPricing(AddOrEditPricingDto pricingDto);

        ResultDto DeletePricing(long id);
        ResultDto<List<GetAllPricingDto>> GetAllPricingForAdmin();
        ResultDto<List<GetAllPricingDto>> GetAllPricingForّIndex();

        ResultDto<AddOrEditPricingDto> Fill(long id);
    }

    public class PricingServices : IPricingServices
    {
        private IDataBaseContext _context;
        public PricingServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<AddOrEditPricingDto> AddOrEditPricing(AddOrEditPricingDto pricingDto)
        {
            if (pricingDto.Id == 0)
            {
                if (pricingDto.Amount == null || string.IsNullOrWhiteSpace(pricingDto.Amount) ||
                    string.IsNullOrWhiteSpace(pricingDto.Description) || string.IsNullOrWhiteSpace(pricingDto.Title))
                {
                    return new ResultDto<AddOrEditPricingDto>()
                    {
                        Message = "اطلاعات مورد نیاز را وارد کنید",
                        IsSuccess = false
                    };
                }


                Domain.Models.Pricing pricing = new Domain.Models.Pricing()
                {
                    Description = pricingDto.Description,
                    Title = pricingDto.Title,
                    Amount = pricingDto.Amount,
                    IsRemoved = false,
                };

                _context.Pricings.Add(pricing);
                _context.SaveChanges();

                return new ResultDto<AddOrEditPricingDto>()
                {
                    Message = "با موفقیت ثبت شد",
                    IsSuccess = true
                };
            }
            else
            {
                var pricing = _context.Pricings.FirstOrDefault(x => x.Id == pricingDto.Id);
                if (pricing == null)
                {
                    return new ResultDto<AddOrEditPricingDto>()
                    {
                        Message = "یافت نشد",
                        IsSuccess = false
                    };
                }

                pricing.Amount = pricingDto.Amount;
                pricing.Description = pricingDto.Description;
                pricing.Title = pricingDto.Title;
                _context.SaveChanges();

                return new ResultDto<AddOrEditPricingDto>()
                {
                    Message = "با موفقیت ویرایش شد",
                    IsSuccess = true
                };
            }



        }

        public ResultDto DeletePricing(long id)
        {
            if (id == 0 || id == null)
            {
                return new ResultDto()
                {
                    Message = "یافت نشد",
                    IsSuccess = false,
                };
            }

            var res = _context.Pricings.FirstOrDefault(p => p.Id == id);

            if (res == null)
            {
                return new ResultDto()
                {
                    Message = "یافت نشد",
                    IsSuccess = false,
                };
            }

            res.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "با موفقیت حذف شد"
            };
        }

        public ResultDto<List<GetAllPricingDto>> GetAllPricingForAdmin()
        {

            return new ResultDto<List<GetAllPricingDto>>
            {
                Data = _context.Pricings.Where(x => x.IsRemoved == false)
                    .Select(x => new GetAllPricingDto()
                    {
                        Description = x.Description,
                        Title = x.Title,
                        Amount = x.Amount,
                        Id = x.Id
                    }).ToList(),
                Message = "",
                IsSuccess = true
            };
        }

        public ResultDto<List<GetAllPricingDto>> GetAllPricingForّIndex()
        {
            return new ResultDto<List<GetAllPricingDto>>
            {
                Data = _context.Pricings.Where(x => x.IsRemoved == false)
                    .Select(x => new GetAllPricingDto()
                    {
                        Description = x.Description,
                        Title = x.Title,
                        Amount = x.Amount,

                    }).ToList(),
                Message = "",
                IsSuccess = true
            };
        }

        public ResultDto<AddOrEditPricingDto> Fill(long id)
        {

            if (id == 0 || id == null)
            {
                return new ResultDto<AddOrEditPricingDto>()
                { Data = new AddOrEditPricingDto() { Id = 0 }, IsSuccess = false, Message = "یافت نشد" };
            }

            return new ResultDto<AddOrEditPricingDto>()
            {
                Data = _context.Pricings
                    .Select(x=>new AddOrEditPricingDto()
                    {
                        Description = x.Description,
                        Title = x.Title,
                        Amount = x.Amount,
                        Id = x.Id
                    }).FirstOrDefault(x=>x.Id==id),
                Message = "",
                IsSuccess = true
            };

        }
    }

    public class AddOrEditPricingDto
    {
        public int? Id { get; set; }

        public string Amount { get; set; }

        public string Description { get; set; } = null!;

        public string Title { get; set; } = null!;

    }
    public class GetAllPricingDto
    {
        public int? Id { get; set; }

        public string? Amount { get; set; }

        public string? Description { get; set; }

        public string? Title { get; set; }

    }
}
