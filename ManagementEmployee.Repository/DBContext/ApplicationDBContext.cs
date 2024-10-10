using ManagementEmployee.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementEmployee.Repository.DBContext
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Users> Users { get; set; }
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
           
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Users>().HasData(

                new Users
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    Password = "admin",
                    Role = "Admin"
                },
                new Users
                {
                    Id = 2,
                    Email = "manager@gmail.com",
                    Password = "manager",
                    Role = "Manager"
                },
                new Users
                {
                    Id = 3,
                    Email = "user@gmail.com",
                    Password = "user",
                    Role = "User"
                });

        }
    }
}

