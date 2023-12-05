using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocHub.Core.Domain.Entities;

public class Appointment
{
    [Key] public Guid Id { get; set; }
    public Guid? PatientId { get; set; }
    [ForeignKey("PatientId")] public Patient? Patient { get; set; }
    public string? TestProp { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}