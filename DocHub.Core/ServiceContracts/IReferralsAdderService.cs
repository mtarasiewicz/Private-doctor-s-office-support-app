using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IReferralsAdderService
{
    Task<Task> AddRange(List<ReferralAddRequest> referralAddRequestsAddRequests);
}