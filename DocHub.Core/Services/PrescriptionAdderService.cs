using System.Web.WebPages;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using ValidationHelper = DocHub.Core.Helpers.ValidationHelper;

namespace DocHub.Core.Services;

public class PrescriptionAdderService : IPrescriptionAdderService
{
    private readonly IPrescriptionRepository _repository;

    public PrescriptionAdderService(IPrescriptionRepository repository)
    {
        _repository = repository;
    }
    public async Task<Task> AddRange(List<PrescriptionAddRequest> prescriptionAddRequests)
    {
        if (!prescriptionAddRequests.Any()) return Task.CompletedTask;
        List<Prescription> list = prescriptionAddRequests
            .Select(tmp => tmp.ToPrescription()).ToList();
        foreach (var item in list)
        {
            item.Id = Guid.NewGuid();
        }

        await _repository.AddRange(list);
        return Task.CompletedTask;
    }
}