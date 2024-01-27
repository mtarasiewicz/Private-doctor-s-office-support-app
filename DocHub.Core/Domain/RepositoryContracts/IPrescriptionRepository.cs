using DocHub.Core.Domain.Entities;

namespace DocHub.Core.Domain.RepositoryContracts;

public interface IPrescriptionRepository
{
    Task<List<Prescription>> AddRange(List<Prescription> prescriptions);
}