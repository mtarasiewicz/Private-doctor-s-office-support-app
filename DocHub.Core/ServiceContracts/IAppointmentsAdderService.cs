using DocHub.Core.Domain.Entities;
using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IAppointmentsAdderService
{
    Task<AppointmentResponse> Add(AppointmentAddRequest? request);
}