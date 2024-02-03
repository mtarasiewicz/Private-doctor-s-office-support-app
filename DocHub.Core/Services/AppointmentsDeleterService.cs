using DocHub.Core.Domain.Entities;
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
    public async Task<AppointmentResponse> Delete(Guid id)
    {
        Appointment? appointment = await _appointmentsRepository.Get(id);
        if (appointment is null) throw new InvalidOperationException();
        await _appointmentsRepository.Delete(appointment: appointment);
        return appointment.ToAppointmentResponse();
    }

    public async Task<int> DeleteRange(List<Guid> ids)
    {
        List<Appointment> appointments = new List<Appointment>();
        foreach (var id in ids)
        {
            var appointment = await _appointmentsRepository.Get(id);
            if(appointment is null ) continue;
            appointments.Add(appointment);
        }
        await _appointmentsRepository.RemoveRange(appointments);

        return appointments.Count;
    }
    
}