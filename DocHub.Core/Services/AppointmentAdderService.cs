using System.Web.Mvc;
using System.Web.WebPages;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using ValidationHelper = DocHub.Core.Helpers.ValidationHelper;

namespace DocHub.Core.Services;

public class AppointmentAdderService : IAppointmentsAdderService
{
    private readonly IAppointmentsRepository _appointmentsRepository;

    public AppointmentAdderService(IAppointmentsRepository appointmentsRepository)
    {
        _appointmentsRepository = appointmentsRepository;
    }
    public async Task<AppointmentResponse> Add(AppointmentAddRequest request)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));
        ValidationHelper.ModelValidation(request);
        Appointment appointment = request.ToAppointment();
        appointment.Id = Guid.NewGuid();
        await _appointmentsRepository.Create(appointment);
        return appointment.ToAppointmentResponse();
    }
}