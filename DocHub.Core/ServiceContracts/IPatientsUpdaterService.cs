using DocHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.ServiceContracts
{
    public interface IPatientsUpdaterService
    {
        Task<PatientResponse> UpdatePatient(PatientUpdateRequest? request);
    }
}
