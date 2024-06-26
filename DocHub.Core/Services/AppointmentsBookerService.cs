using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.Helpers;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class AppointmentsBookerService : IAppointmentsBookerService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsBookerService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }

    public async Task<AppointmentResponse> Reserve(AppointmentReserveRequest? appointmentReserveRequest)
    {
        if (appointmentReserveRequest is null)
        {
            throw new ArgumentNullException(nameof(appointmentReserveRequest));
        }

        ValidationHelper.ModelValidation(appointmentReserveRequest);
        Appointment? matchingAppointment = await _appointmentsRepository.Get(appointmentReserveRequest.Id);
        if (matchingAppointment is null)
        {
            throw new ArgumentException(nameof(matchingAppointment));
        }

        if (matchingAppointment.PatientId is not null)
        {
            throw new ArgumentException("Appointments is already booked");
        }

        matchingAppointment.PatientId = appointmentReserveRequest.PatientId;
        matchingAppointment.State = State.Reserved.ToString();
        await _appointmentsRepository.Edit(matchingAppointment);
        return matchingAppointment.ToAppointmentResponse();
    }

    public async Task<AppointmentResponse> CancelReservation(Guid id)
    {
        Appointment? matchingAppointment = await _appointmentsRepository.Get(id);
        if (matchingAppointment is null)
        {
            throw new ArgumentException(nameof(matchingAppointment));
        }

        if (matchingAppointment.PatientId is null)
        {
            throw new InvalidOperationException($"PatientId is null method:{nameof(CancelReservation)}");
        }

        matchingAppointment.PatientId = null;
        matchingAppointment.State = State.Available.ToString();
        await _appointmentsRepository.Edit(matchingAppointment);
        return matchingAppointment.ToAppointmentResponse();

    }
}