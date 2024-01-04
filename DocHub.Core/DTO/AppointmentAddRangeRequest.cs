using System.ComponentModel.DataAnnotations;

namespace DocHub.Core.DTO;

public class AppointmentAddRangeRequest
{
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
    [Required]
    public int Duration { get; set; }
    [Required]
    public int StartHour { get; set; }
    [Required]
    public int EndHour { get; set; }
}