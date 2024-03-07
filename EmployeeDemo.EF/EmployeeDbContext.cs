using EmployeeDemo.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext>options):base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
