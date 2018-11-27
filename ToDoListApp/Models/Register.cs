using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Username is not specified")]
        [MinLength(4, ErrorMessage = "Minimum length is 4 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [MinLength(8, ErrorMessage = "Minimum length is 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
