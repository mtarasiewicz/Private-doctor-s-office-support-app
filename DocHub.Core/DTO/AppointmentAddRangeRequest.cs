using System.ComponentModel.DataAnnotations;
using DocHub.Core.ValidationAttributes;

namespace DocHub.Core.DTO;

public class AppointmentAddRangeRequest
{
    [Required] public DateTime? StartDate { get; set; }

    [Required]
    [RangeDateValidation("StartDate")]
    public DateTime? EndDate { get; set; }

    [Required] [Range(1, 1400)] public int Duration { get; set; }
    [Required] [Range(0, 23)] public int StartHour { get; set; }

    [Required]
    [Range(1, 24)]
    [WorkHours("StartHour")]
    public int EndHour { get; set; }

    public bool IncludeSaturdays { get; set; } = false;
    public bool IncludeSundays { get; set; } = false;
}