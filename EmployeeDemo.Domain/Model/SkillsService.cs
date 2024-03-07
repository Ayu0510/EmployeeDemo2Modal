using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Model
{
    public class SkillsService : ISkillsService
    {
        private readonly ISkillsRepository _skillsRepository;
        public SkillsService(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }
        public Task<Skill> DeleteSkills(int? id)
        {
            return _skillsRepository.DeleteSkills(id);
        }
    }
}
