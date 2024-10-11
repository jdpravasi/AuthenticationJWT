using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementEmployee.Entity.Validators
{
    public class IsEmployeeAdultValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            var age = DateTime.Now.Year - date.Year;
            if (age >= 18)
            {
                return true;
            }
            return false;
        }
    }
}
