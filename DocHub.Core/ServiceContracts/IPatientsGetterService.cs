using DocHub.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.ServiceContracts
{
    public interface IPatientsGetterService
    {
        Task<PatientResponse?> Get(Guid? id);
        Task<PatientResponse?> GetByUserId(Guid? userId);
        Task<List<PatientResponse>?> GetAll();
    }
}
