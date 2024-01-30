using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class PatientsSearcherService : IPatientsSearcherService
{
    private readonly IPatientsGetterService _patientsGetterService;

    public PatientsSearcherService(IPatientsGetterService patientsGetterService)
    {
        _patientsGetterService = patientsGetterService;
    }

    public async Task<IEnumerable<PatientResponse>?> Search(string? query)
    {
        var allPatients = await _patientsGetterService.GetAll();
        if (allPatients is null) return null;
        if (query is null) return allPatients;


        var test = allPatients.Where(p
            =>
            (p.FullName is not null && p.FullName.Contains(query)) ||
            (p.PeselNumber is not null && p.PeselNumber.Contains(query)) ||
            (p.Email is not null && p.Email.Contains(query)));
        return test;
    }
}