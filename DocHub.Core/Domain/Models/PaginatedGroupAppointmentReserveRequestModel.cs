using DocHub.Core.DTO;

namespace DocHub.Core.Domain.Models;

public class PaginatedGroupAppointmentReserveRequestModel
{
    public PaginatedGroup<DateTime?, AppointmentResponse>? PaginatedGroup { get; set; }
    public AppointmentReserveRequest? AppointmentReserveRequest { get; set; }
}