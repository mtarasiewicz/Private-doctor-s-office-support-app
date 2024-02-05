using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Helpers;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Services
{
    public class PatientsUpdaterService : IPatientsUpdaterService
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsUpdaterService(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        public async Task<PatientResponse> UpdatePatient(PatientUpdateRequest? request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            ValidationHelper.ModelValidation(request);
            Patient? matchingPatient = await _patientsRepository.Get(request.Id);
            if (matchingPatient is null) { throw new Exception("err"); }
            matchingPatient.FirstName = request.FirstName;
            matchingPatient.LastName = request.LastName;
            matchingPatient.Email = request.Email;
            matchingPatient.PhoneNumber = request.PhoneNumber;
            matchingPatient.PeselNumber = request.PeselNumber;
            matchingPatient.Address = request.Address;
            matchingPatient.City = request.City;
            matchingPatient.DateOfBirth= request.DateOfBirth;
            matchingPatient.PostalCode = request.PostalCode;
            matchingPatient.HistoryOfDiseases = request.HistoryOfDiseases;
            matchingPatient.TakenMedications= request.TakenMedications;
            matchingPatient.Allergies = request.Allergies;

           
            await _patientsRepository.Update(matchingPatient);
            return matchingPatient.ToPatientResponse();
        }
        
    }
}
