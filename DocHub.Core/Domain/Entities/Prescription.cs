using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocHub.Core.Domain.Entities;

public class Prescription
{
    [Key] public Guid Id { get; set; }
    public Guid AppointmentId { get; set; }
    [ForeignKey("AppointmentId")] public Appointment? Appointment { get; set; }
    [StringLength(4)] public required string AccessCode { get; set; }
    public string? Information { get; set; }
}