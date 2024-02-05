using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DocHub.Core.Services
{
    public class PatientsGetterService : IPatientsGetterService
    {
        private readonly IPatientsRepository _patientsRepository;

        public PatientsGetterService(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }

        /// <summary>
        /// Searches the repository to find a patient with the identifier (guid) specified in the method parameter.
        /// </summary>
        /// <param name="id">Patient identifier in guid form, may be null</param>
        /// <returns>In the case of null given as a method parameter, it returns the null value, in the case of an identifier that was not found in the repository, it returns the null value, in the case of a value found in the repository, it returns the Patient object in the form <see cref="PatientResponse"/></returns>
        public async Task<PatientResponse?> Get(Guid? id)
        {
            if (id is null) return null;
            Patient? patient = await _patientsRepository.Get(id);
            if (patient == null) return null;
            return patient.ToPatientResponse();
        }

        public async Task<List<PatientResponse>?> GetAll()
        {
            List<Patient>? patients = await _patientsRepository.GetAll();
            if (patients is null) return null;
            return patients.Select(p => p.ToPatientResponse()).ToList();
        }

        public async Task<IQueryable<PatientResponse>>? GetAllAsQueryable()
        {
            var allPatients = await _patientsRepository.GetAll();
            if (allPatients is null) return null;
            return allPatients.Select(patient => patient.ToPatientResponse()).AsQueryable();
        }

        public async Task<PatientResponse?> GetByUserId(Guid? userId)
        {
            if (userId is null) return null;
            Patient? patient = await _patientsRepository.GetByUserId(userId);
            if (patient == null) return null;
            return patient.ToPatientResponse();
        }
    }
}