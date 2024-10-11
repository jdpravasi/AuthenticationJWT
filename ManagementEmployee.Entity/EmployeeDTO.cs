using ManagementEmployee.Entity.Validators;
using System.ComponentModel.DataAnnotations;

namespace ManagementEmployee.Entity
{
    public class EmployeeDTO
    {
        [Key]
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "First Name should be atlease 3 character long")]
        [MaxLength(10, ErrorMessage = "First Name cannot be more than 10 character long")]
        public string FirstName { get; set; }

        [StringLength(10, MinimumLength = 3, ErrorMessage = "Last Name should be atlease 3 character and not more than 10 character")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Designation should be atlease 3 character and not more than 10 character")]
        [Required(ErrorMessage ="Designatin is Reuired")]
        [ConcurrencyCheck]
        public string Designation { get; set; }
        [Required]
        [Range(1000.00, 140000, ErrorMessage = "Salary Range is 1000 to 1 Lakh 40 thousand")]
        public decimal Salary { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        //[RegularExpression(@"[1-9]\\d$", ErrorMessage ="only digits allowed")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        // custom validation
        [IsEmployeeAdultValidator(ErrorMessage = "Employee should be 18 years or above")]
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
