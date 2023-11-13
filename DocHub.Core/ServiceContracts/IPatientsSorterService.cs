using DocHub.Core.DTO;
using DocHub.Core.Enums.Views;

namespace DocHub.Core.ServiceContracts;

public interface IPatientsSorterService
{
    Task<IEnumerable<PatientResponse>> GetSortedPatients(
        IEnumerable<PatientResponse> patients,
        string sortBy, SortOrderOptions sortOrder);
}