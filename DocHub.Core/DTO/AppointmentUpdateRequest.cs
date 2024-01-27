using System.ComponentModel.DataAnnotations;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Enums.Appointments;

namespace DocHub.Core.DTO;

public class AppointmentUpdateRequest
{
    public Guid Id { get; set; }

    public string? TestProp { get; set; }
    [Required]
    public string? Interview { get; set; }
    [Required]
    public string? Diagnosis { get; set; }
    public string? Recommendations { get; set; }
    public string? Notes { get; set; }
    public bool? Finished { get; set; }
    public State? State { get; set; }
    public List<PrescriptionAddRequest>? Prescriptions { get; set; }
}