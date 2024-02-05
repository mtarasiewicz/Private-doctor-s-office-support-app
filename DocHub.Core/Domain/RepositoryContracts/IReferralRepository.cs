using DocHub.Core.Domain.Entities;

namespace DocHub.Core.Domain.RepositoryContracts;

public interface IReferralRepository
{
    Task<List<Referral>> AddRange(List<Referral> prescriptions);
    List<Referral> GetAllByAppointmentId(Guid appointmentId);
}