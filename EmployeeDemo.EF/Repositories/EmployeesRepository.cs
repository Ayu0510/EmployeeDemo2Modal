using EmployeeDemo.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeDbContext _dbContext;
        public EmployeesRepository(EmployeeDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await GetQuery().ToListAsync();
        
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return employee;
        }

        public async Task<Employee> GetEmployeeById(int? id)
        {
            return await GetQuery().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Employee>> GetEmployeeByName(string name)
        {
            return await GetQuery().Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).ToListAsync();
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            Employee employee = await GetEmployeeById(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return employee;
        }

        public Task<Employee> UpdateEmployee(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return Task.FromResult(employee);
        }

        private IQueryable<Employee> GetQuery()
        {
            return _dbContext.Employees.AsNoTracking()
                .Include(x => x.Skills)
                .AsQueryable();
        }
    }
}
