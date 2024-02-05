using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace DocHub.Core.DTO;

public class AppointmentResponse
{
    public Guid Id { get; set; }
    public Guid? PatientId { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool? Finished { get; set; } 
    public State? State { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public string? PatientName { get; set; }
    public string? PatientLastName { get; set; }
    public string? Interview { get; set; }
    public string? Diagnosis { get; set; }
    public string? Recommendations { get; set; }
    public string? Notes { get; set; }
    public List<PrescriptionResponse>? Prescriptions { get; set; }
    public List<ReferralResponse>? Referrals { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() != typeof(AppointmentResponse)) return false;
        var other = obj as AppointmentResponse;
        return this.Id == other?.Id;
    }

    public AppointmentReserveRequest ToAppointmentReserveRequest() => new AppointmentReserveRequest()
    {
        Id = this.Id,
        PatientId = this.PatientId,
    };

    public void SetPrescriptions(List<PrescriptionResponse> prescriptionResponses)
    {
        this.Prescriptions = prescriptionResponses;
    } 
    public void SetReferrals(List<ReferralResponse> referralResponses)
    {
        this.Referrals = referralResponses;
    }

    public AppointmentUpdateRequest ToAppointmentUpdateRequest() => new AppointmentUpdateRequest()
    {
        Id = this.Id,
        State = this.State,
        Finished = this.Finished,
        Interview = this.Interview,
        Diagnosis = this.Diagnosis,
        Recommendations = this.Recommendations,
        Notes = this.Notes
    };

    public Appointment ToAppointment() => new Appointment()
    {
        Id = this.Id,
        PatientId = this.PatientId,
        Interview = this.Interview,
        Diagnosis = this.Diagnosis,
        Start = this.Start,
        End = this.End,
        Finished = this.Finished,
        State = this.State.ToString() ?? Enums.Appointments.State.Available.ToString(),
        Notes = this.Notes
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
                IsAvailable = appointment.PatientId is null,
                Start = appointment.Start,
                End = appointment.End,
                PatientName = appointment.Patient?.FirstName,
                PatientLastName = appointment.Patient?.LastName,
                Finished = appointment.Finished,
                State = Enum.Parse<State>(appointment.State),
                Recommendations = appointment.Recommendations,
                Interview = appointment.Interview,
                Diagnosis = appointment.Diagnosis,
                Notes = appointment.Notes,
            };
    }
}