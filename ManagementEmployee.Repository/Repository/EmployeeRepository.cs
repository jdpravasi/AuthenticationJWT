using ManagementEmployee.Entity;
using ManagementEmployee.Interface;
using ManagementEmployee.Repository.DBContext;
using ManagementEmployee.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementEmployee.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;
        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ResponseModel> AddEmployeeAsync(EmployeeDTO employee)
        {
            Employee EmpToADD = new()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Designation = employee.Designation,
                Salary = employee.Salary,
                DOB = DateTime.SpecifyKind(employee.DateOfBirth.Date, DateTimeKind.Utc),
                CreatedAt = DateTime.UtcNow.Date,
                UpdatedAt = DateTime.UtcNow.Date,
                Gender = employee.Gender,
            };

            await _context.Employee.AddAsync(EmpToADD);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseModel
                {
                    Success = true,
                    Data = EmpToADD.Id,
                    Message = "Employee Added successfully"
                };
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Unable to Add Employee"
            };
        }

        public async Task<ResponseModel> DeleteEmployeeAsync(int id)
        {
            Employee employee = _context.Employee.FirstOrDefault(emp => emp.Id == id);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {id} not found.");
            }
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return new ResponseModel
            {
                Success = true,
                Message = "Deleted Successfully"
            };
        }

        public async Task<ResponseModel> GetAllEmployeesAsync()
        {
            List<Employee> empList = await _context.Employee.ToListAsync();
            if (empList.Count == 0 || empList == null)
            {
                throw new ArgumentException("No Employee found.");
            }
            List<EmployeeDTO> empDTOList = empList.Select(emp => new EmployeeDTO
            {
                Id = emp.Id,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Designation = emp.Designation,
                Salary = emp.Salary,
                Gender = emp.Gender,
                DateOfBirth = DateTime.SpecifyKind(emp.DOB.Date, DateTimeKind.Utc),
                CreatedAt = DateTime.SpecifyKind(emp.CreatedAt.Date, DateTimeKind.Utc),
                UpdatedAt = DateTime.SpecifyKind(emp.UpdatedAt.Date, DateTimeKind.Utc)
            }).ToList();

            return new ResponseModel
            {
                Success = true,
                Data = empDTOList,
                Message = "Employee List"
            };
        }

        public async Task<ResponseModel> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(emp => emp.Id == id);
            if (employee != null)
            {

                EmployeeDTO employeeDTO = new EmployeeDTO
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Designation = employee.Designation,
                    Salary = employee.Salary,
                    Gender = employee.Gender,
                    DateOfBirth = DateTime.SpecifyKind(employee.DOB.Date, DateTimeKind.Utc),
                    CreatedAt = DateTime.SpecifyKind(employee.CreatedAt.Date, DateTimeKind.Utc),
                    UpdatedAt = DateTime.SpecifyKind(employee.UpdatedAt.Date, DateTimeKind.Utc)
                };
                return new ResponseModel
                {
                    Success = true,
                    Data = employeeDTO,
                    Message = "Employee Found"
                };
            }

            throw new ArgumentException($"Employee with ID {id} is found.");
        }

        public async Task<ResponseModel> UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            Employee EMP = await _context.Employee.FindAsync(id);
            if (EMP == null)
            {
                throw new ArgumentException($"Employee with ID {id} not found.");
            }
            EMP.FirstName = employee.FirstName.Trim();
            EMP.LastName = employee.LastName.Trim();
            EMP.Email = employee.Email.Trim();
            EMP.Designation = employee.Designation.Trim();
            EMP.Salary = employee.Salary;
            EMP.Gender = employee.Gender.Trim();
            EMP.DOB = DateTime.SpecifyKind(employee.DateOfBirth, DateTimeKind.Utc);
            EMP.CreatedAt = DateTime.SpecifyKind(EMP.CreatedAt, DateTimeKind.Utc);
            EMP.UpdatedAt = DateTime.UtcNow;
            _context.Employee.Update(EMP);
            if (await _context.SaveChangesAsync() > 0)
            {
                return new ResponseModel
                {
                    Data = EMP.Id,
                    Success = true,
                    Message = "Employee Updated",
                };
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Unable to Update Employee"
            };
        }
        public Task<ResponseModel> Login(string email, string password)
        {
            var user = _context.Users.Where(Users => Users.Email == email && Users.Password == password).FirstOrDefault();
            if (user != null) {
                return Task.FromResult(new ResponseModel
                {
                    Success = true,
                    Message = "Logged in Succesfully",
                    Data = user
                });
            }
            return Task.FromResult(new ResponseModel
            {
                Success = false,
                Message = "Invalid Credentials"
            });
        }
    }
}
