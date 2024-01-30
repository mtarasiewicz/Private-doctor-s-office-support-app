using AutoFixture;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using DocHub.Core.Services;
using FluentAssertions;
using Moq;

namespace DocHub.PatientsServiceTests;

public class AppointmentsAdderServiceTest
{
    private readonly Mock<IAppointmentsRepository> _mockAppointmentsRepository;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IAppointmentsAdderService _appointmentsAdderService;
    private readonly IFixture _fixture;

    public AppointmentsAdderServiceTest()
    {
        _mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
        _appointmentsRepository = _mockAppointmentsRepository.Object;
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _appointmentsAdderService = new AppointmentAdderService(_appointmentsRepository);
    }

    [Fact]
    public async Task Add_RequestIsNull_ShouldThrowArgumentNullException()
    {
        AppointmentAddRequest? request = null;
        Func<Task> action = async () => await _appointmentsAdderService.Add(request);
        await action.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Add_RequestIsCorrect_ShouldBeSuccessful()
    {
        var request = _fixture.Build<AppointmentAddRequest>().Create();
        var appointment = request.ToAppointment();
        _mockAppointmentsRepository
            .Setup(method => method.Create(It.IsAny<Appointment>()))
            .ReturnsAsync(appointment);
        var expectedAppointment = appointment.ToAppointmentResponse();

        _mockAppointmentsRepository
            .Setup(method => method.Create(It.IsAny<Appointment>()))
            .ReturnsAsync(appointment);

        var response = await _appointmentsAdderService.Add(request);
        expectedAppointment.Id = response.Id;

        response.Id.Should().NotBe(Guid.Empty);
        response.Should().Be(expectedAppointment);

    }
}