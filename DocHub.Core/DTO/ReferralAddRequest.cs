using DocHub.Core.Domain.Entities;

namespace DocHub.Core.DTO;

public class ReferralAddRequest
{
    public int? FirstDigit { get; set; }
    public int? SecondDigit { get; set; }
    public int? ThirdDigit { get; set; }
    public int? FourthDigit { get; set; }
    public string? Information { get; set; }
    public Guid AppointmentId { get; set; }

    public Referral ToReferral() => new Referral()
    {
        AccessCode = String.Concat(this.FirstDigit, this.SecondDigit, this.ThirdDigit, this.FourthDigit),
        Information = this.Information,
        AppointmentId = this.AppointmentId,
    };
}