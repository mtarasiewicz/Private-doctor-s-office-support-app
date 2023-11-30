using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Helpers;
using DocHub.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Services
 {
    public class PatientsAdderService : IPatientsAdderService
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsAdderService(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        public async Task<PatientResponse> AddPatient(PatientAddRequest request)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            ValidationHelper.ModelValidation(request);
            Patient patient = request.ToPatient();
            patient.Id = Guid.NewGuid();
            await _patientsRepository.Add(patient);
            return patient.ToPatientResponse();
        }
    }
}
