using ManagementEmployee.Entity;
using ManagementEmployee.Interface;

namespace ManagementEmployee.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public async Task<ResponseModel> AddEmployeeAsync(EmployeeDTO employee)
        {
            if(employee.Salary < 0)
            {
                throw new ArithmeticException("Salary cannot be negative");
            }

            return await _employeeRepo.AddEmployeeAsync(employee);
        }
        public async Task<ResponseModel> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepo.DeleteEmployeeAsync(id);
        }
        public async Task<ResponseModel> GetAllEmployeesAsync()
        {
            return await _employeeRepo.GetAllEmployeesAsync();
        }
        public async Task<ResponseModel> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepo.GetEmployeeByIdAsync(id);
        }

        public async Task<ResponseModel> UpdateEmployeeAsync(int id, EmployeeDTO employee)
        {
            return await _employeeRepo.UpdateEmployeeAsync(id, employee);
        }

        public Task<ResponseModel> Login(string email, string password)
        {
            return _employeeRepo.Login(email, password);
        }
    }
}
