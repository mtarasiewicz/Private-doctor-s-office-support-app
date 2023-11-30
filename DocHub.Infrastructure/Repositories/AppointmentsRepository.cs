using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
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
        await _dbContext.SaveChangesAsync();
        return matchingAppointment;
    }
}