using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class PrescriptionsGetterService : IPrescriptionGetterService
{
    private readonly IPrescriptionRepository _repository;

    public PrescriptionsGetterService(IPrescriptionRepository repository)
    {
        _repository = repository;
    }
    public List<PrescriptionResponse> GetAllByAppointmentId(Guid appointmentId)
    {
        var list = _repository.GetAllByAppointmentId(appointmentId);
        return list.Select(item => item.ToPrescriptionResponse()).ToList();
    }
}