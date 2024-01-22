using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IAppointmentUpdaterService
{
    Task<AppointmentResponse> Update(AppointmentUpdateRequest? request);
}