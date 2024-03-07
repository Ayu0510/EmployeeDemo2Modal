using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.Domain.Model
{
    public class EmployeesService : IEmployeesService
    {
        public IEmployeesRepository _EmployeeRepository;
        public EmployeesService(IEmployeesRepository employeesRepository) 
        {
            _EmployeeRepository = employeesRepository;
        }

        public Task<List<Employee>> GetAllEmployees()
        {
            return _EmployeeRepository.GetAllEmployees();
        }
        public Task<Employee> AddEmployee(Employee employee)
        {
            return _EmployeeRepository.AddEmployee(employee);
        }
        public async Task<Employee> GetEmployeeById(int? id)
        {
            return await _EmployeeRepository.GetEmployeeById(id);
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            return await _EmployeeRepository.DeleteEmployee(id);
        }

        public Task<Employee> UpdateEmployee(Employee employee)
        {
            return _EmployeeRepository.UpdateEmployee(employee);
        }
        public Task<List<Employee>> GetEmployeeByName(string? name)
        {
            return _EmployeeRepository.GetEmployeeByName(name);
        }
    }
}
