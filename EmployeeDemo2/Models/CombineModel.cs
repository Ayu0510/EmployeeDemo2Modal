using EmployeeDemo.Domain.Model;

namespace EmployeeDemo.web.Models
{
    public class CombineModel
    {
        public EmployeeModelView Employee { get; set; }
        public skillsModelView Skill { get; set; }
    }
}
