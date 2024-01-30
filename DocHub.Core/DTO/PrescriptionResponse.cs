using DocHub.Core.Domain.Entities;

namespace DocHub.Core.DTO;

public class PrescriptionResponse
{
    public Guid Id { get; set; }
    public string? AccessCode { get; set; }
    public string? Information { get; set; }
    public Guid? AppointmetnId { get; set; }
}
public static class PrescriptionResponseExtensions
{
    public static PrescriptionResponse ToPrescriptionResponse(this Prescription prescription)
    {
        return new PrescriptionResponse()
        {
            Id = prescription.Id,
            AccessCode = prescription.AccessCode,
            Information = prescription.Information,
            AppointmetnId = prescription.AppointmentId
        };
    }
}