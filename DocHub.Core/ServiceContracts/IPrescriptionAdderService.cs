using DocHub.Core.DTO;

namespace DocHub.Core.ServiceContracts;

public interface IPrescriptionAdderService
{
    Task<Task> AddRange(List<PrescriptionAddRequest> prescriptionAddRequests);
}