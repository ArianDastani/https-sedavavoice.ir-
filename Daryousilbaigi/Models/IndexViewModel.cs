using Application.Services.Education;
using Application.Services.Information;
using Application.Services.Portfolio;
using Application.Services.Skills;

namespace Daryousilbaigi.Models
{
    public class IndexViewModel
    {
        public GetInfoDto? GetInfo { get; set; }
        public List<GetSkillsForIndexDto>? SkillsForIndex { get; set; }
        public List<ResultEducationForindexDto>? EducationForindex { get; set; }

        public List<GetAllportfolioForIndexDto>? GetAllPortfolioForIndex { get; set; }
    }
}
