using DocHub.Core.Domain.Entities;

namespace DocHub.Core.DTO;

public class AppointmentAddRequest
{
    public string? TestProp { get; set; }

    public Appointment ToAppointment()
    {
        return new Appointment()
        {
            TestProp = this.TestProp,
        };
    }
}