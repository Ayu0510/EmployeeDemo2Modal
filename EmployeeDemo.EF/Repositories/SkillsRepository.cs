using EmployeeDemo.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public SkillsRepository(EmployeeDbContext employeeDbContext) 
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<List<Skill>> GetSkills()
        {
            return await _employeeDbContext.Skills.AsNoTracking().ToListAsync();
             
        }
        public async Task<Skill> AddSkills(Skill skills)
        {
            _employeeDbContext.Skills.Add(skills);
            _employeeDbContext.SaveChanges();
            return skills;
        }

        public async Task<List<Skill>> FindById(int? id)
        {
            return await _employeeDbContext.Skills.AsNoTracking().Where(x => x.EmployeeId == id).ToListAsync();

        }

        public async Task<Skill> DeleteSkills(int? id)
        {
            List<Skill> skills = await FindById(id);

            foreach (Skill sk in skills)
            {
                _employeeDbContext.Skills.Remove(sk);
            }

            await _employeeDbContext.SaveChangesAsync();

            return skills.FirstOrDefault();
        }
    }
}
