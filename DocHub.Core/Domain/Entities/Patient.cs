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
        [StringLength(40)]
        public string? Email { get; set; }
        [StringLength(20)]
        public string? PhoneNumber {  get; set; }
        [StringLength(30)]
        public string? FirstName { get; set; }
        [StringLength(40)]
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(11)]
        public string? PeselNumber { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(6)]
        public string? PostalCode { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        public string? TakenMedications { get; set; }
        public string? HistoryOfDiseases { get; set; }
        public string? Allergies { get; set; }
        public Guid? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
