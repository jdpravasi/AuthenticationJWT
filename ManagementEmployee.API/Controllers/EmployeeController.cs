using ManagementEmployee.Entity;
using ManagementEmployee.Interface;
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
        [Authorize]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("api/employee/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        [Route("api/employee")]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDTO employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("api/employee/{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, [FromBody] EmployeeDTO employee)
        {
            await _employeeService.UpdateEmployeeAsync(id, employee);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("api/employee/{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
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
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ThisismySecretKEYFORAuthentication!@%!@#$*&!@*#(7^#$(*&!@#$*^(!@#^$(*&!@#$(*&11234987+asjfl;aHFAJKD2139408-1==asdfjk;asd**&(*&asdfj;aklsjdflk;");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            response.Data = tokenString;
            return Ok(response);
        }

   
    }
}