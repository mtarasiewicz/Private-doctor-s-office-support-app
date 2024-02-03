using System.Web.Mvc;
using AutoFixture;
using DocHub.Core.Domain.Entities.IdentityEntities;
using DocHub.Core.Domain.Models;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using DocHub.Core.Services;
using DocHub.Ui.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Moq;
using ViewResult = Microsoft.AspNetCore.Mvc.ViewResult;

namespace DocHub.ControllersTests;

public class AppointmentsControllerTest
{
    private readonly IAppointmentsGetterService _appointmentsGetterService;
    private readonly IAppointmentsAdderService _appointmentsAdderService;
    private readonly IAppointmentsBookerService _appointmentsBookerService;
    private readonly IPatientsGetterService _patientsGetterService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IAppointmentsAddRangeService _appointmentsAddRangeService;
    private readonly IAppointmentUpdaterService _appointmentUpdaterService;
    private readonly IPrescriptionAdderService _prescriptionAdderService;
    private readonly IPrescriptionGetterService _prescriptionGetterService;
    private readonly IEmailSenderService _emailSender;
    private readonly IAppointmentsDeleterService _appointmentsDeleterService;
    private readonly Mock<IAppointmentsDeleterService> _mockDeleter;
    private readonly Mock<IAppointmentsGetterService> _mockGetter;
    private readonly Mock<IAppointmentsAdderService> _mockAdder;
    private readonly Mock<IAppointmentsRepository> _mockRepository;
    private readonly Mock<IAppointmentsBookerService> _mockBooker;
    private readonly Mock<IPatientsGetterService> _mockPatientsGetter;
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<IAppointmentsAddRangeService> _mockAddRange;
    private readonly Mock<IAppointmentUpdaterService> _mockUpdater;
    private readonly Mock<IPrescriptionGetterService> _mockPrescriptionGetter;
    private readonly Mock<IPrescriptionAdderService> _mockPrescriptionAdder;
    private readonly Mock<IEmailSenderService> _mockEmailService;

    private IFixture _fixture;

    public AppointmentsControllerTest()
    {
        _fixture = new Fixture();
        _mockGetter = new Mock<IAppointmentsGetterService>();
        _mockAdder = new Mock<IAppointmentsAdderService>();
        _mockRepository = new Mock<IAppointmentsRepository>();
        _mockBooker = new Mock<IAppointmentsBookerService>();
        _mockPatientsGetter = new Mock<IPatientsGetterService>();
        _mockUserManager = Mock.Of<Mock<UserManager<ApplicationUser>>>();
        _mockAddRange = new Mock<IAppointmentsAddRangeService>();
        _mockUpdater = new Mock<IAppointmentUpdaterService>();
        _mockPrescriptionGetter = new Mock<IPrescriptionGetterService>();
        _mockPrescriptionAdder = new Mock<IPrescriptionAdderService>();
        _mockEmailService = new Mock<IEmailSenderService>();
        _mockDeleter = new Mock<IAppointmentsDeleterService>();

        _appointmentsGetterService = _mockGetter.Object;
        _appointmentsRepository = _mockRepository.Object;
        _appointmentsAdderService = _mockAdder.Object;
        _appointmentUpdaterService = _mockUpdater.Object;
        _userManager = _mockUserManager.Object;
        _appointmentsBookerService = _mockBooker.Object;
        _patientsGetterService = _mockPatientsGetter.Object;
        _appointmentsAddRangeService = _mockAddRange.Object;
        _prescriptionAdderService = _mockPrescriptionAdder.Object;
        _prescriptionGetterService = _mockPrescriptionGetter.Object;
        _emailSender = _mockEmailService.Object;
        _appointmentsDeleterService = _mockDeleter.Object;

        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
    
    [Fact]
    public async Task Index_ShouldReturnViewWithAppointmentsCollection()
    {
        var appointments = new List<AppointmentResponse>();
        for (int i = 0; i < 20; i++)
        {
            appointments.Add(_fixture.Build<AppointmentResponse>().With(app => app.Start, DateTime.Now.AddDays(i)).Create());
        }
        var patients = _fixture.Create<List<PatientResponse>>();
        AppointmentsController controller =
            new(_appointmentsGetterService, _appointmentsAdderService, _appointmentsBookerService,
                _patientsGetterService, _userManager, _appointmentsRepository, _appointmentsAddRangeService,
                _appointmentUpdaterService, _emailSender, _prescriptionAdderService, _prescriptionGetterService, _appointmentsDeleterService);
        
        _mockGetter.Setup(method => method.GetAll()).ReturnsAsync(appointments);
        _mockPatientsGetter.Setup(method => method.GetAll()).ReturnsAsync(patients);
        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        viewResult.ViewData.Model.Should().BeAssignableTo<PaginatedGroup<DateTime?, AppointmentResponse>>();
    }
    
    
}