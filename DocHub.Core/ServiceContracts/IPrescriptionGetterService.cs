using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IPrescriptionGetterService
{
    List<PrescriptionResponse> GetAllByAppointmentId(Guid appointmentId);
}