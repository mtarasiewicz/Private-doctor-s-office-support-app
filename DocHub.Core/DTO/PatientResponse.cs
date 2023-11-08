using DocHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.DTO
{
    public class PatientResponse
    {
        public Guid? Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PeselNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? TakenMedications {  get; set; }
        public string? HistoryOfDseases { get; set; }
        public string? Allergies { get; set; }

    }

    public static class PatientExtenstions
    { 
        public static PatientResponse ToPatientResponse(this Patient patient)
        {
            return new PatientResponse()
            {
                Id = patient.Id,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                PeselNumber = patient.PeselNumber,
                City = patient.City,
                Address = patient.Address,
                TakenMedications = patient.TakenMedications,
                HistoryOfDseases = patient.HistoryOfDiseases,
                PostalCode = patient.PostalCode,
                Allergies = patient.Allergies
            };

        }
    }

}
