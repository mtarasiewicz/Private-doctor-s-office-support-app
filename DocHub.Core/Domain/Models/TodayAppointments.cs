using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;

namespace DocHub.Core.Domain.Models;

public class TodayAppointments
{
    public IOrderedEnumerable<AppointmentResponse>? Appointments { get; }

    public TodayAppointments(List<AppointmentResponse>? appointments)
    {
        if (appointments != null)
            this.Appointments = appointments
                .OrderBy(model => model.State == State.During ? 0 : (model.State == State.Reserved ? 1 : 2))
                .ThenBy(
                    app =>
                    {
                        if (app.Start != null) return app.Start.Value;
                        throw new ArgumentException();
                    });
    }

    public int Count
    {
        get
        {
            if (Appointments != null) return Appointments.Count();
            return 0;
        }
    }

    public int CompletedAppointmetns
    {
        get
        {
            if (Appointments != null) return Appointments.Count(model => model.State == State.Finished);
            return 0;
        }
    }

    public int AppointmentsLeft => Count - CompletedAppointmetns;

    public bool IsDuring(AppointmentResponse appointmentResponse)
    {
        return appointmentResponse.State is State.During;
    }

    public bool IsAnyDuring()
    {
        if (Appointments is not null) return Appointments.Any(app => app.State is State.During);
        return false;
    }
}