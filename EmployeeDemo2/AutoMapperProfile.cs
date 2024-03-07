using AutoMapper;
using EmployeeDemo.Domain.Model;
using EmployeeDemo.web.Models;
using skillsModelView = EmployeeDemo.web.Models.skillsModelView;

namespace EmployeeDemo.web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(IWebHostEnvironment _webHostEnvironment)
        {
            CreateMap<Employee, EmployeeModelView>()
                .ForMember(dest => dest.SkillNames, opt => opt.MapFrom(src => string.Join(",", src.Skills.Select(x => x.SkillName).ToList())))
                .ForMember(dest => dest.AllSkills, opt => opt.MapFrom(src => src.Skills.Select(x => x.SkillName).ToList()));
            CreateMap<EmployeeModelView, Employee>();
            CreateMap<skillsModelView, Skill>();
            CreateMap<Skill, skillsModelView>();
        }
    }
}
