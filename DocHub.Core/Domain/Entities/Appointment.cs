using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocHub.Core.DTO;

namespace DocHub.Core.Domain.Entities;

public class Appointment
{
    [Key] public Guid Id { get; set; }
    public Guid? PatientId { get; set; }
    [ForeignKey("PatientId")] public Patient? Patient { get; set; }
    public string? Interview { get; set; }
    public string? Diagnosis { get; set; }
    public string? Recommendations { get; set; }
    public string? Notes { get; set; }
    public string State { get; set; } = Enums.Appointments.State.Available.ToString();
    public bool? Finished { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public virtual List<Prescription>? Prescriptions { get; set; }
    public virtual List<Referral>? Referrals { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != typeof(Appointment)) return false;

        var other = obj as Appointment;
        return this.Id == other?.Id;
    }
}