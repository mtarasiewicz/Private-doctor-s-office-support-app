using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class ReferralsAdderService : IReferralsAdderService
{
    private readonly IReferralRepository _repository;

    public ReferralsAdderService(IReferralRepository repository)
    {
        _repository = repository;
    }
    public async Task<Task> AddRange(List<ReferralAddRequest> referralAddRequestsAddRequests)
    {
        if (!referralAddRequestsAddRequests.Any()) return Task.CompletedTask;
        List<Referral> list = referralAddRequestsAddRequests
            .Select(tmp => tmp.ToReferral()).ToList();
        foreach (var item in list)
        {
            item.Id = Guid.NewGuid();
        }

        await _repository.AddRange(list);
        return Task.CompletedTask;
    }
}