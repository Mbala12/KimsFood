using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderItem.ViewModel
{
    public class SignUpViewModel
    {
        public int AccountID { get; set; }

        //[Display(Name = "UserName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName required")]
        public string Username { get; set; }

        //[Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address required")]
        public string EmailID { get; set; }

        //[Display(Name = "Phone No.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No. required")]
        [MinLength(10, ErrorMessage = "Phone No. must contain 10 digits")]
        public string Phone { get; set; }

        //[Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        public string Password { get; set; }

        //[Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm Password and Password do not match")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password required")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "User Role")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Role required")]
        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
}