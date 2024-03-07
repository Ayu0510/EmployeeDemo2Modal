using System.ComponentModel.DataAnnotations;

namespace EmployeeDemo.Domain.Model
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string? SkillName { get; set; }
        public int EmployeeId {  get; set; }
    }
}
