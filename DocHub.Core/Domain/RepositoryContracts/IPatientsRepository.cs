using DocHub.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Domain.RepositoryContracts
{
    public interface IPatientsRepository
    {
        Task<Patient> Add(Patient patient);
        Task<Patient?> Get(Guid? id);
        Task<Patient?> GetByUserId(Guid? userId);

        Task<Patient> Update(Patient patient);
        Task<List<Patient>?> GetAll();

    }
}
