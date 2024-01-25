using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class AppointmentsDeleterService : IAppointmentsDeleterService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsDeleterService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }
    public Task<AppointmentResponse> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
    
}