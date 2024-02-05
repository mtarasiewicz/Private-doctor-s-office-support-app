using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IReferralsGetterService
{
    List<ReferralResponse> GetAllByAppointmentId(Guid appointmentId);
}