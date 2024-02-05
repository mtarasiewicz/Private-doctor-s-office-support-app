using DocHub.Core.Domain.Entities;

namespace DocHub.Core.DTO;

public class ReferralResponse
{
    public Guid Id { get; set; }
    public string? AccessCode { get; set; }
    public string? Information { get; set; }
    public Guid? AppointmentId { get; set; }
}
public static class ReferralResponseExtensions
{
    public static ReferralResponse ToReferralResponseResponse(this Referral referral)
    {
        return new ReferralResponse()
        {
            Id = referral.Id,
            AccessCode = referral.AccessCode,
            Information = referral.Information,
            AppointmentId = referral.AppointmentId
        };
    }
}