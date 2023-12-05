using DocHub.Core.Domain.Entities;
using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IAppointmentsGetterService
{
    Task<List<AppointmentResponse>?> GetAll();
    Task<AppointmentResponse?> Get(Guid? id);
    
}