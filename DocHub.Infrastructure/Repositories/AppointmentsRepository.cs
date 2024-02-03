using System.Net;
using System.Net.Mail;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DocHub.Infrastructure.Repositories;

public class AppointmentsRepository : IAppointmentsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AppointmentsRepository(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Appointment> Create(Appointment appointment)
    {
        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync();
        return appointment;
    }

    public async Task<Appointment?> Get(Guid? id)
    {
        return await _dbContext.Appointments.Where(appointment =>
            appointment.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Appointment>?> GetAll()
    {
        return await _dbContext.Appointments.ToListAsync();
    }

    public async Task<Appointment> Edit(Appointment appointment)
    {
        Appointment? matchingAppointment =
            await _dbContext.Appointments
                .FirstOrDefaultAsync(temp =>
                    temp.Id == appointment.Id
                );
        if (matchingAppointment is null)
            return appointment;
        matchingAppointment.TestProp = appointment.TestProp;
        matchingAppointment.Patient = appointment.Patient;
        matchingAppointment.PatientId = appointment.PatientId;
        matchingAppointment.State = appointment.State;
        matchingAppointment.Finished = appointment.Finished;
        matchingAppointment.Recommendations = appointment.Recommendations;
        matchingAppointment.Diagnosis = appointment.Diagnosis;
        matchingAppointment.Notes = appointment.Notes;
        matchingAppointment.Interview = appointment.Interview;
        await _dbContext.SaveChangesAsync();
        return matchingAppointment;
    }

    public IQueryable<AppointmentResponse?> GetAllAsViewModels() =>
        _dbContext.Appointments.Select(appointment => appointment.ToAppointmentResponse());


    public async Task<List<Appointment>> AddRange(List<Appointment> appointments)
    {
        _dbContext.Appointments.AddRange(appointments);
        await _dbContext.SaveChangesAsync();
        return appointments;
    }

    public async Task<List<Appointment>> GetAllReservedByDate(DateTime appointmentDate)
    {
        return await _dbContext.Appointments
            .Where(app => app.Start != null && app.Start.Value.Date == appointmentDate && app.PatientId != null)
            .Include(app => app.Patient).ToListAsync();
    }

    public async Task<List<Appointment>> GetAllFinishedPatientAppointments(Guid patientId)
    {
        return await _dbContext.Appointments.Where(app => app.PatientId == patientId && app.State == State.Finished.ToString()).ToListAsync();
    }

    public async Task<Appointment> Delete(Appointment appointment)
    {
        _dbContext.Appointments.Remove(appointment);
        await _dbContext.SaveChangesAsync();
        return appointment;
    }

    public async Task<List<Appointment>> RemoveRange(List<Appointment> appointments)
    {
        _dbContext.Appointments.RemoveRange(appointments);
        await _dbContext.SaveChangesAsync();
        return appointments;
       
    }

    public async Task<List<Appointment>> GetAllPatientsAppointments(Guid? patientId)
    {
        return await _dbContext.Appointments.Where(appointment => appointment.PatientId == patientId).ToListAsync();
    }
}