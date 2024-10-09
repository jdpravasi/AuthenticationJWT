using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ManagementEmployee.Entity
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    //public class EmployeeDTOValidator : AbstractValidator<EmployeeDTO>
    //{
    //    public EmployeeDTOValidator()
    //    {
    //        RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("FirstName Can not be Empty or Null");
    //    }
    //}
}
