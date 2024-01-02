using Application.Interface.Context;
using Application.Services.Skills;
using Common.ResultDto;


namespace Application.Services.Education
{
    public interface IEducationService
    {
        ResultDto AddOrEditEducation(AddOrEditEducationDto skillsDto);

        ResultDto<AddOrEditEducationDto> GetEducationById(long id);

        ResultDto<AddOrEditEducationDto> FillEducation(long id);

        ResultDto<List<AddOrEditEducationDto>> GetAllEducationForAdmin();
        ResultDto<List<ResultEducationForindexDto>> GetAllEducationForIndex();


        ResultDto DeleteEducation(long id);
    }

    public class EducationService : IEducationService
    {
        private IDataBaseContext _context;
        public EducationService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddOrEditEducation(AddOrEditEducationDto EducationDto)
        {
            if (EducationDto.Id == 0)
            {
                Domain.Models.Education education = new Domain.Models.Education
                {
                    EndDate = EducationDto.EndDate,
                    EducationPlace = EducationDto.EducationPlace,
                    Title = EducationDto.Title,
                };

                _context.Educations.Add(education);
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "اطلاعات با موفقیت ذخیره شد"
                };
            }
            else
            {
                var education = _context.Educations.FirstOrDefault(x => x.Id == EducationDto.Id);
                if (education == null) { return new ResultDto { IsSuccess = false, Message = "یافت نشد" }; }

                education.Title = EducationDto.Title;
                education.EndDate = EducationDto.EndDate;
                education.EducationPlace = EducationDto.EducationPlace;
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "تغییرات با موفقیت ذخیره شد"
                };

            }
        }

        public ResultDto DeleteEducation(long id)
        {
            var edu = _context.Educations.FirstOrDefault(x => x.Id == id);
            if (edu == null) { return new ResultDto { IsSuccess = false, Message = "یافت نشد" }; }

            edu.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "با موفقیت انجام شد"
            };
        }

        public ResultDto<AddOrEditEducationDto> FillEducation(long id)
        {
            if (id == 0)
            {

                return new ResultDto<AddOrEditEducationDto>
                {
                    Data = new AddOrEditEducationDto { Id = 0 }
                };

            }


            var edu = _context.Educations.FirstOrDefault(x => x.Id == id);

            if (edu == null)
            {
                return new ResultDto<AddOrEditEducationDto>
                {
                    Data = new AddOrEditEducationDto { Id = 0 }
                };


            }

            return new ResultDto<AddOrEditEducationDto>
            {
                Data = new AddOrEditEducationDto
                {
                    EndDate = edu.EndDate,
                    EducationPlace = edu.EducationPlace,
                    Id = edu.Id,
                    Title = edu.Title,
                },
                IsSuccess = true,
                Message = ""
            };

        }

        public ResultDto<List<AddOrEditEducationDto>> GetAllEducationForAdmin()
        {
            var edus = _context.Educations
                .Where(x => x.IsRemoved == false)
                .Select(x => new AddOrEditEducationDto
                {
                    EndDate = x.EndDate,
                    EducationPlace = x.EducationPlace,
                    Id = x.Id,
                    Title = x.Title,
                }).ToList();

            return new ResultDto<List<AddOrEditEducationDto>>
            {
                Data = edus,
                IsSuccess = true,
                Message = ""
            };
        }

        public ResultDto<List<ResultEducationForindexDto>> GetAllEducationForIndex()
        {

            return new ResultDto<List<ResultEducationForindexDto>>
            {
                Data=_context.Educations.Where(x=>x.IsRemoved == false)
                .Select(x=>new ResultEducationForindexDto
                {
                    EndDate= x.EndDate,
                    EducationPlace= x.EducationPlace,
                    Title= x.Title,
                })
                .ToList(),
            };
            
        }

        public ResultDto<AddOrEditEducationDto> GetEducationById(long id)
        {
            var edu = _context.Educations.FirstOrDefault(x => x.Id == id);

            if (edu == null) { return new ResultDto<AddOrEditEducationDto> { IsSuccess = false, Message = "یافت نشد" }; }

            return new ResultDto<AddOrEditEducationDto>
            {
                Data = new AddOrEditEducationDto { EndDate = edu.EndDate, EducationPlace = edu.EducationPlace, Id = edu.Id, Title = edu.Title },
                IsSuccess = true,
                Message = ""

            };
        }
    }

    public class AddOrEditEducationDto
    {
        public int? Id { get; set; }

        public string EducationPlace { get; set; } = null!;

        public DateTime EndDate { get; set; }

        public string Title { get; set; } = null!;

    }

    public class ResultEducationForindexDto
    {
        public string? EducationPlace { get; set; } 

        public DateTime EndDate { get; set; }

        public string? Title { get; set; }

    }

}
