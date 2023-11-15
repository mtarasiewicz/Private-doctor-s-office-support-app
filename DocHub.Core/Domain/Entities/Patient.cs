using DocHub.Core.Domain.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Domain.Entities
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber {  get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PeselNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? TakenMedications { get; set; }
        public string? HistoryOfDiseases { get; set; }
        public string? Allergies { get; set; }
        public Guid? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
