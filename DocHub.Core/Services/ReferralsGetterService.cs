using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class ReferralsGetterService : IReferralsGetterService
{
    private readonly IReferralRepository _repository;

    public ReferralsGetterService(IReferralRepository repository)
    {
        _repository = repository;
    }
    public List<ReferralResponse> GetAllByAppointmentId(Guid appointmentId)
    {
        var list = _repository.GetAllByAppointmentId(appointmentId);
        return list.Select(item => item.ToReferralResponseResponse()).ToList();
    }
}