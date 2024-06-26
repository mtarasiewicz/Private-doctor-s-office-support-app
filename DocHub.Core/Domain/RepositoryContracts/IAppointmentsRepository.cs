using DocHub.Core.Domain.Entities;
using DocHub.Core.DTO;

namespace DocHub.Core.Domain.RepositoryContracts;

public interface IAppointmentsRepository
{
    Task<Appointment> Create(Appointment appointment);
    Task<Appointment?> Get(Guid? id);
    Task<List<Appointment>?> GetAll();
    Task<Appointment> Edit(Appointment appointment);

    IQueryable<AppointmentResponse?> GetAllAsViewModels();

    Task<List<Appointment>> AddRange(List<Appointment> appointments);

    Task<List<Appointment>> GetAllReservedByDate(DateTime appointmentDate);

    Task<List<Appointment>> GetAllFinishedPatientAppointments(Guid patientId);

    Task<Appointment> Delete(Appointment appointment);
    Task<List<Appointment>> RemoveRange(List<Appointment> appointments);
    Task<List<Appointment>> GetAllPatientsAppointments(Guid? patientId);

}