using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.ServiceContracts;

namespace DocHub.Core.Services;

public class AppointmentsAddRangeService : IAppointmentsAddRangeService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsAddRangeService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }
    public async Task<List<Appointment>> AddRange(AppointmentAddRangeRequest request)
    {
        if(request.StartDate is null || request.EndDate is null) throw new ArgumentNullException("nie ma daty");
        var appointments =
            GenerateVisit(request.StartDate.Value, request.EndDate.Value, request.Duration, request.StartHour, request.EndHour);
        await _appointmentsRepository.AddRange(appointments);
        return appointments;
    }

    private List<Appointment> GenerateVisit(DateTime startDate, DateTime endDate, int duration,
        int startHour, int endHour)
    {
        List<Appointment> visits = new List<Appointment>();
        DateTime currentDay = startDate.Date;

        while (currentDay <= endDate.Date)
        {
            DateTime currentTime = currentDay.AddHours(startHour); 

            while (currentTime.AddMinutes(duration) <= currentDay.AddHours(endHour)) 
            {
                DateTime endTime = currentTime.AddMinutes(duration);
                if (IsAppointmentValid(currentTime, endTime))
                {
                    visits.Add(new Appointment()
                    {
                        Id = Guid.NewGuid(),
                        Start = currentTime,
                        End = endTime,
                        State = State.Available.ToString(),
                    });
                }

                currentTime = endTime;
            }
            currentDay = currentDay.AddDays(1); 
        }
        return visits;
    }
    private bool IsAppointmentValid(DateTime start, DateTime end)
    {
        var appointments = _appointmentsRepository.GetAll().Result;
        if (appointments != null)
        {
            var existingAppointments = appointments;

            foreach (var app in existingAppointments)
            {
                if (IsTimeOverlap(app.Start, app.End, start, end))
                {
                    return false; 
                }
            }
        }

        return true; 
    }
    private bool IsTimeOverlap(DateTime? existingStart, DateTime? existingEnd, DateTime newStart, DateTime newEnd)
    {
        return (newStart < existingEnd && newEnd > existingStart);
    }

}