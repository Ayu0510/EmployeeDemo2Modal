using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Model
{
    public interface ISkillsRepository
    {
        Task<Skill> DeleteSkills(int? id);
    }
}
