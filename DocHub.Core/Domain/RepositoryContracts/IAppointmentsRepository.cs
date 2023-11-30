using DocHub.Core.Domain.Entities;

namespace DocHub.Core.Domain.RepositoryContracts;

public interface IAppointmentsRepository
{
    Task<Appointment> Create(Appointment appointment);
    Task<Appointment?> Get(Guid? id);
    Task<List<Appointment>?> GetAll();
    Task<Appointment> Edit(Appointment appointment);
}