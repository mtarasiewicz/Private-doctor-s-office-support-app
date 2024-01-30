using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IPatientsSearcherService
{
    Task<IEnumerable<PatientResponse>?> Search(string query);
}