using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Registration
    {
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is requierd")]
        public string FirstName { get; set; }
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is requierd")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is requierd")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Need min 6 character")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is requierd")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password should match with Password")]
        public string ConfirmPassword { get; set; }
    }
}