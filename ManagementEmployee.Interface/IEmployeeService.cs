using ManagementEmployee.Entity;

namespace ManagementEmployee.Interface
{
    public interface IEmployeeService
    {
        Task<ResponseModel> GetAllEmployeesAsync();
        Task<ResponseModel> GetEmployeeByIdAsync(int id);
        Task<ResponseModel> AddEmployeeAsync(EmployeeDTO employee);
        Task<ResponseModel> UpdateEmployeeAsync(int id, EmployeeDTO employee);
        Task<ResponseModel> DeleteEmployeeAsync(int id);
        Task<ResponseModel> Login(string email, string password);
    }
}
