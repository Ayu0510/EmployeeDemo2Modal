using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Model
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int? id);
        Task<Employee> DeleteEmployee(int id);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<List<Employee>> GetEmployeeByName(string? name);
    }
}