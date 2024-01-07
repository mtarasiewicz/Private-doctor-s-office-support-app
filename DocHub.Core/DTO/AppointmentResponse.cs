using System.Runtime.CompilerServices;
using DocHub.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DocHub.Core.DTO;

public class AppointmentResponse
{
    public Guid? Id { get; set; }
    public string? TestProp { get; set; }
    public Guid? PatientId { get; set; }
    public bool IsAvailable { get; set; } = true;

    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? PatientName { get; set; }
    public string? PatientLastName { get; set; }

    public AppointmentReserveRequest ToAppointmentReserveRequest() => new AppointmentReserveRequest()
    {
        Id = this.Id,
        PatientId = this.PatientId,
    };
}

public static class AppointmentExtensions
{
    public static AppointmentResponse ToAppointmentResponse(this Appointment appointment)
    {
        return new AppointmentResponse()
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            TestProp = appointment.TestProp,
            IsAvailable = appointment.PatientId is null,
            Start = appointment.Start,
            End = appointment.End,
            PatientName = appointment.Patient?.FirstName,
            PatientLastName = appointment.Patient?.LastName,
        };
    }
}