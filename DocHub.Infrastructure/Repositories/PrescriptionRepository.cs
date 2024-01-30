using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Infrastructure.DbContext;

namespace DocHub.Infrastructure.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PrescriptionRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Prescription>> AddRange(List<Prescription> prescriptions)
    {
        _dbContext.AddRange(prescriptions);
        await _dbContext.SaveChangesAsync();
        return prescriptions;
    }

    public List<Prescription> GetAllByAppointmentId(Guid appointmentId)
    {
        return _dbContext.Prescriptions.Where(pres => pres.AppointmentId == appointmentId).ToList();
    }
}