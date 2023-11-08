using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email can't be blank"),
        EmailAddress(ErrorMessage = "Email should be in a proper email addres format"),
        DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password can't be blank"),
            DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
