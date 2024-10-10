using ManagementEmployee.Entity;
using ManagementEmployee.Interface;
using ManagementEmployee.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ManagementEmployee.API.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("api/employee")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("api/employee/{id}")]
        [Authorize(Roles = "User")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        [Route("api/employee")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDTO employee)
        {
           var response =  await _employeeService.AddEmployeeAsync(employee);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        [Route("api/employee/{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, [FromBody] EmployeeDTO employee)
        {
           var response =  await _employeeService.UpdateEmployeeAsync(id, employee);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Manager")]
        [Route("api/employee/{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var response =  await _employeeService.DeleteEmployeeAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/employee/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _employeeService.Login(loginRequest.Email, loginRequest.Password);
            if (!response.Success)
            {
                return Unauthorized(response.Message);
            }
            var user = response.Data as Users;  // for role based authentication
            if (user == null)
            {
                return Unauthorized("Invalid User data");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("!@%!@#$*&!@*#(7^#$(*&!@#$*^(!@#^$(*&!@#$(*&11234987+asjfl;aHFAJKD2139408-1==asdfjk;asd**&(*&asdfj;aklsjdflk;");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, loginRequest.Email),
                    new Claim(ClaimTypes.Role,user.Role) // role claim added for role base authentication
                ]),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            response.Data = tokenString;
            return Ok(response);
        }
    }
}