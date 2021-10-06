using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Master_table
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is requierd")]
        public string name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID is requierd")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is requierd")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Need min 6 character")]
        public string Password { get; set; }

        public string role { get; set; }
    }
}