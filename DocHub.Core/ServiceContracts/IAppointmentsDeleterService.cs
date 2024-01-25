using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IAppointmentsDeleterService
{
    Task<AppointmentResponse> Delete(Guid id);
    
}