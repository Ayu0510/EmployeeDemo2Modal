using EmployeeDemo.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDemo.EF.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Email);
            builder.Property(x => x.Gender);
            builder.Property(x => x.DateOfBirth);
            builder.Property(x => x.Designation);
            builder.Property(x => x.DateOfJoining);
            builder.Property(x => x.Image);
            builder.Property(x => x.Description);
            
            builder
                .HasMany(e => e.Skills)
                .WithOne()
                .HasForeignKey(e => e.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
