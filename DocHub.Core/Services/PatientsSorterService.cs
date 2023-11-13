using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Views;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class PatientsSorterService : IPatientsSorterService
{
    private readonly IPatientsRepository _patientsRepository;

    public PatientsSorterService(IPatientsRepository patientsRepository)
    {
        _patientsRepository = patientsRepository;
    }
    public async Task<IEnumerable<PatientResponse>> GetSortedPatients(
        IEnumerable<PatientResponse> patients, 
        string sortBy, 
        SortOrderOptions sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy)) return patients;

        var sortedPatients = (sortBy, sortOrder)
            switch
            {
                (nameof(PatientResponse.FullName), SortOrderOptions.Asc)
                    => patients.OrderBy(patient => patient.FullName, StringComparer.OrdinalIgnoreCase),
                (nameof(PatientResponse.FullName), SortOrderOptions.Desc)
                    => patients.OrderByDescending(patient => patient.FullName, StringComparer.OrdinalIgnoreCase),
                _ => patients
            };
        return sortedPatients;
    }
}