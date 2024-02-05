using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Infrastructure.DbContext;

namespace DocHub.Infrastructure.Repositories;

public class ReferralsRepository : IReferralRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReferralsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Referral>> AddRange(List<Referral> prescriptions)
    {
        _dbContext.Referrals.AddRange(prescriptions);
        await _dbContext.SaveChangesAsync();
        return prescriptions;
    }

    public List<Referral> GetAllByAppointmentId(Guid appointmentId)
    {
        return _dbContext.Referrals.Where(a => a.AppointmentId == appointmentId).ToList();
    }
}