using DocHub.Core.Enums;
using DocHub.Core.Enums.Identity;
using DocHub.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.DTO
{
    public class EditAccountRequest
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Fist name can't be blank")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last name can't be blank")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email address can't be blank")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        //TODO: [Remote()]
        public string? Email { get; set; }
        [PeselNumber(ErrorMessage = "Zły")]
        public string? PeselNumber { get; set; }
        [Required(ErrorMessage = "Phone number can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = ("Phone number should contain only digits"))]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? TakenMedications { get; set; }
        public string? HistoryOfDiseases { get; set; }
        public string? Allergies { get; set; }
    }
}
