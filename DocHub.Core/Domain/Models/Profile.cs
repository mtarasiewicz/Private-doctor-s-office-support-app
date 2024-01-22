using DocHub.Core.DTO;

namespace DocHub.Core.Domain.Models;

public class Profile
{
    public PatientResponse? Patient { get; set; }
    public IEnumerable<AppointmentResponse>? Appointments { get; set; }
}