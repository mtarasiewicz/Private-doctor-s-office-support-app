using DocHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.ServiceContracts
{
    public interface IPatientsAdderService
    {
        Task<PatientResponse> AddPatient(PatientAddRequest request);
    }
}
