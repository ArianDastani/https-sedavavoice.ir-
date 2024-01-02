using Application.Interface.Context;
using Common.ResultDto;
using Domain.Models;


namespace Application.Services.Skills
{
    public interface ISkillServices
    {
        ResultDto AddOrEditSkill(AddOrEditSkillsDto skillsDto);

        ResultDto<AddOrEditSkillsDto> GetSkillById(long id);

        ResultDto<AddOrEditSkillsDto> FillSkill(long id);

        ResultDto<List<AddOrEditSkillsDto>> GetAllSkillsForAdmin();

        ResultDto DeleteSkill(long id);

        ResultDto<List<GetSkillsForIndexDto>> GetSkillsForIndex();
    }
    public class SkillServices : ISkillServices
    {
        private IDataBaseContext _context;
        public SkillServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddOrEditSkill(AddOrEditSkillsDto skillsDto)
        {
            if (skillsDto.Id == 0)
            {
                Skill skill = new Skill
                {
                    Title = skillsDto.Title,
                    Percent = skillsDto.Percent,

                };

                _context.Skills.Add(skill);
                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "با موفقیت اضافه شد"
                };

            }
            else
            {
                var skill = _context.Skills.Find(skillsDto.Id);

                skill.Percent = skillsDto.Percent;
                skill.Title = skillsDto.Title;
                _context.SaveChanges();


                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "با موفقیت ویرایش شد"
                };

            }


        }

        public ResultDto DeleteSkill(long id)
        {
            var skill = _context.Skills.FirstOrDefault(x => x.Id == id);
            skill.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess=true,
                Message="با موفقیت حذف شد"
            };
        }

        public ResultDto<AddOrEditSkillsDto> FillSkill(long id)
        {
            if (id == 0)
            {
                return new ResultDto<AddOrEditSkillsDto>
                {
                    Data = new AddOrEditSkillsDto { Id = 0 }
                };
            }

            var skill = GetSkillById(id);

            if (skill.Data == null)
            {
                return new ResultDto<AddOrEditSkillsDto>
                {
                    Data = new AddOrEditSkillsDto { Id = 0 }
                };
            }

            return new ResultDto<AddOrEditSkillsDto>
            {
                Data=new AddOrEditSkillsDto
                {
                    Id=skill.Data.Id,
                    Percent=skill.Data.Percent,
                    Title=skill.Data.Title,
                }
            };
        }

        public ResultDto<List<AddOrEditSkillsDto>> GetAllSkillsForAdmin()
        {
            var skills = _context.Skills
                .Where(y => y.IsRemoved == false)
                .Select(x => new AddOrEditSkillsDto
                {
                    Id = x.Id,
                    IsRemoved=x.IsRemoved,
                    Percent = x.Percent,
                    Title = x.Title,

                })
                .ToList();

            return new ResultDto<List<AddOrEditSkillsDto>>
            {
                Data = skills,
                IsSuccess = true,
                Message = ""
            };
        }

        public ResultDto<AddOrEditSkillsDto> GetSkillById(long id)
        {
            return new ResultDto<AddOrEditSkillsDto>
            {
                Data = _context.Skills
                .Select(x => new AddOrEditSkillsDto
                {
                    Id = x.Id,
                    Percent = x.Percent,
                    Title = x.Title
                }).FirstOrDefault(y => y.Id == id),
                IsSuccess = true,
                Message = "",
            };
        }

        public ResultDto<List<GetSkillsForIndexDto>> GetSkillsForIndex()
        {
            return new ResultDto<List<GetSkillsForIndexDto>>
            {
                Data = _context.Skills
                .Where(x => x.IsRemoved == false)
                .Select(x => new GetSkillsForIndexDto
                {
                    Percent = x.Percent,
                    Title = x.Title,
                }).ToList(),
                IsSuccess = true,
                Message=""
            };

            
        }
    }
    public class AddOrEditSkillsDto
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Percent { get; set; } = null!;

        public bool? IsRemoved { get; set; }
    }

    public class GetSkillsForIndexDto
    {
        public string? Title { get; set; } 

        public string? Percent { get; set; }

    }
}
