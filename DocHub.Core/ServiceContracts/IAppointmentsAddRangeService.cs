using DocHub.Core.Domain.Entities;
using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IAppointmentsAddRangeService
{
    Task<List<Appointment>> AddRange(AppointmentAddRangeRequest request);
}