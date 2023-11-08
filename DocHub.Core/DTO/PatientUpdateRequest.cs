using DocHub.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.DTO
{
    public class PatientUpdateRequest
    {
        public Guid? Id { get; set; }
        [StringLength(50, ErrorMessage = "First Name must be between {2} and {1} characters.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First Name should contain only letters.")]
        [Required(ErrorMessage = "First name can't be blank")]
        public string? FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Last Name must be between {2} and {1} characters.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last Name should contain only letters.")]
        [Required(ErrorMessage = "Last name cant'be blank")]
        public string? LastName { get; set; }
        [PeselNumber]
        public string? PeselNumber { get; set; }
        [Required(ErrorMessage = "Email address can't be blank")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        //TODO: [Remote()]
        public string? Email { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should contain only digits")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? City { get; set; }
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "The Postal Code field is required and must be in the format XX-XXX.")]
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? TakenMedications { get; set; }
        public string? HistoryOfDiseases { get; set; }
        public string? Allergies { get; set; }
        public Guid? UserId { get; set; }
    }
}
