using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IAppointmentsBookerService
{ 
    Task<AppointmentResponse> Reserve(AppointmentReserveRequest? appointmentReserveRequest);
    Task<AppointmentResponse> CancelReservation(Guid id);
}