using System.Web.WebPages;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.ServiceContracts;
using ValidationHelper = DocHub.Core.Helpers.ValidationHelper;

namespace DocHub.Core.Services;

public class AppointmentsUpdaterService : IAppointmentUpdaterService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentsUpdaterService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }
    public async Task<AppointmentResponse> Update(AppointmentUpdateRequest? request)
    {
        if (request is null) throw new ArgumentNullException();
        ValidationHelper.ModelValidation(request);
        Appointment? matchingAppointment = await _appointmentsRepository.Get(request.Id);
        if (matchingAppointment is null) throw new ArgumentException();
        matchingAppointment.TestProp = request.TestProp;
        matchingAppointment.Notes = request.Notes;
        matchingAppointment.Interview = request.Interview;
        matchingAppointment.Diagnosis = request.Diagnosis;
        matchingAppointment.Recommendations = request.Recommendations;
        if (request.Finished == false)
        {
            matchingAppointment.State = State.During.ToString();
        } else if (request.Finished == true)
        {
            matchingAppointment.State = State.Finished.ToString();
        }
        await _appointmentsRepository.Edit(matchingAppointment);
        return matchingAppointment.ToAppointmentResponse();
    }
}