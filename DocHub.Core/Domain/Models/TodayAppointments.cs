using DocHub.Core.DTO;

namespace DocHub.Core.Domain.Models;

public class TodayAppointments
{
    public IOrderedEnumerable<AppointmentResponse>? Appointments { get;}
    public TodayAppointments(List<AppointmentResponse>? appointments)
    {
        if (appointments != null)
            this.Appointments = appointments.OrderBy(app =>
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
}