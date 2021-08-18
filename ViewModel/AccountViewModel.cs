using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OrderItem.ViewModel
{
    public class AccountViewModel
    {
        //[Display(Name = "UserName")]
        //public string Username { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string Password { get; set; }

        //public int AccountID { get; set; }

        

        //[Display(Name ="Phone No.")]
        //[MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        //public string Phone { get; set; }

        //public bool IsActive { get; set; }

        //[Display(Name = "Confirm Password")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm Password and Password do not match")]
        //public string ConfirmPassword { get; set; }
    }
}