using DocHub.Core.Enums;
using DocHub.Core.Enums.Identity;
using DocHub.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DocHub.Core.DTO
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "First name can't be blank")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last name can't be blank")]
        [MaxLength(50, ErrorMessage = "Max 50 charactes")]
        public required string LastName { get; set;}
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        //TODO: [Remote()]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password can't be blank")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Confirm password can't be blank")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public required string ConfirmPassword { get; set; }
        public UserRoles Role { get; set; } = UserRoles.Admin;
    }
}
