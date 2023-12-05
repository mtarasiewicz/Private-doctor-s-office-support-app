using DocHub.Core.Domain.Entities;

namespace DocHub.Core.DTO;

public class AppointmentAddRequest
{
    public string? TestProp { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public Appointment ToAppointment()
    {
        return new Appointment()
        {
            TestProp = this.TestProp,
            Start = this.Start,
            End = this.End
        };
    }
}