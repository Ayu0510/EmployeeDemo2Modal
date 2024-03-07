using System.ComponentModel.DataAnnotations;

namespace EmployeeDemo.web.Models
{
    public class skillsModelView
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public int EmployeeId {  get; set; }
    }
}
