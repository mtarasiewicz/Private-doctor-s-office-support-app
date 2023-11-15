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
        public async Task<PatientResponse?> Get(Guid? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
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

        public async Task<PatientResponse?> GetByUserId(Guid? userId)
        {
            if (userId is null) throw new ArgumentNullException(nameof(userId));
            Patient? patient = await _patientsRepository.GetByUserId(userId); 
            if (patient == null) return null;
            return patient.ToPatientResponse();
        }
    }
}
