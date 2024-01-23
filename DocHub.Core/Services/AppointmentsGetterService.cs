using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class AppointmentsGetterService : IAppointmentsGetterService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsGetterService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }
    public async Task<List<AppointmentResponse>?> GetAll()
    {
        var allAppointments = await _appointmentsRepository.GetAll();
        if (allAppointments is null) return null;
        return allAppointments
            .Select
                (appointment => appointment.ToAppointmentResponse())
            .ToList();
    }

    public async Task<AppointmentResponse?> Get(Guid? id)
    {
        if (id is null) return null;
        Appointment? appointment = await _appointmentsRepository.Get(id.Value);
        if (appointment is null) return null;
        return appointment.ToAppointmentResponse();
    }

    public async Task<List<AppointmentResponse>?> GetAllReservedByDate(DateTime appointmentDate)
    {
        var appointments = await _appointmentsRepository.GetAllReservedByDate(appointmentDate);
        return appointments.Select(appointment => appointment.ToAppointmentResponse()).ToList();
    }

    public async Task<List<AppointmentResponse>> GetAllFinishedPatientsAppointments(Guid patientId)
    {
        var finishedAppointments = await _appointmentsRepository.GetAllFinishedPatientAppointments(patientId);
        return finishedAppointments.Select(app => app.ToAppointmentResponse()).ToList();
    }
}