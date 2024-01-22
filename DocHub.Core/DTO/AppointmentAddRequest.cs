using System.ComponentModel.DataAnnotations;
using DocHub.Core.Domain.Entities;

namespace DocHub.Core.DTO;

public class AppointmentAddRequest
{
    [Required] public DateTime Start { get; set; }

    [RegularExpression(@"^\d+$", ErrorMessage = "Duration time must be numeric")]
    [Required(ErrorMessage = "The duration of the visit is required")]
    [Range(1, 1400, ErrorMessage = "Duration time must be greater than 0")]
    public required int Duration { get; set; }

    [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "The appointment time must be a clock value")]
    public required string Hour { get; set; }

    public Appointment ToAppointment()
    {
        string[] hourMinute = Hour.Split(":");
        int hour = Convert.ToInt32(hourMinute[0]);
        int minute = Convert.ToInt32(hourMinute[1]);
        var startDate = new DateTime(Start.Year, Start.Month, Start.Day, hour, minute, 0);
        return new Appointment()
        {
            Start = startDate,
            End = startDate.AddMinutes(Duration)
        };
    }
}