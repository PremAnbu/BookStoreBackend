using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO.Request
{
    public class UserRequest
    {
        [Required(ErrorMessage = " Name Should Not Be Empty")]
        public String name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Id is required")]
        public String email { get; set; }

        [PasswordPropertyText]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d!@#$%^&*]{8,16}$")]
        public String password { get; set; }

        [Required(ErrorMessage ="Mobile Number is requires")]
        [RegularExpression("^\\d{10}$")]
        public long mobileNumber { get; set; }
    }
}
