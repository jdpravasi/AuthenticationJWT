using ManagementEmployee.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementEmployee.Repository.DBContext
{
    public class EmployeeContext(DbContextOptions<EmployeeContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Jignesh",
                    LastName = "Pravasi",
                    Email = "jignesh@gmail.com",
                    Designation = "Software Engineer",
                    CreatedAt = DateTime.UtcNow.Date,
                    DOB = DateTime.UtcNow.Date,
                    Gender = "Male",
                    Salary = 10000,
                    UpdatedAt = DateTime.UtcNow.Date
                });
        }
    }
}

