using AutoFixture;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.Enums.Appointments;
using DocHub.Core.ServiceContracts;
using DocHub.Core.Services;
using FluentAssertions;
using Moq;

namespace DocHub.PatientsServiceTests;

public class AppointmentsGetterServiceTest
{
    private readonly Mock<IAppointmentsRepository> _mockAppointmentsRepository;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IAppointmentsGetterService _appointmentsGetterService;
    private readonly IFixture _fixture;

    public AppointmentsGetterServiceTest()
    {
        _mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
        _appointmentsRepository = _mockAppointmentsRepository.Object;
        _appointmentsGetterService = new AppointmentsGetterService(_appointmentsRepository);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    #region GetAll()
    [Fact]
    public async Task GetAll_CollectionIsEmpty_ShouldReturnNull()
    {
        var emptyAppointments = new List<Appointment>();
        _mockAppointmentsRepository
            .Setup(method => method.GetAll())
            .ReturnsAsync(emptyAppointments);
        var expectedAppointments = await _appointmentsGetterService.GetAll();
        expectedAppointments.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_CollectionWithFewRecord_ShouldReturnCollectionOfAppointments()
    {
        var appointments = new List<Appointment>();
        for (int i = 0; i < 10; i++)
        {
            appointments.Add(_fixture.Build<Appointment>()
                .With(appointment => appointment.State, State.Available.ToString)
                .Create());
        }

        _mockAppointmentsRepository
            .Setup(method => method.GetAll())
            .ReturnsAsync(appointments);

        var expectedAppointments = await _appointmentsGetterService.GetAll();
        
        expectedAppointments.Should().NotBeEmpty();
        expectedAppointments.Should()
            .BeEquivalentTo(appointments.Select(appointment => appointment.ToAppointmentResponse()));
    }

    #endregion

    #region Get()

    [Fact]
    public async Task Get_IdIsNull_ShouldReturnNull()
    {
        Guid? id = null;
        var expectedAppointment = await _appointmentsGetterService.Get(id);
        expectedAppointment.Should().BeNull();
    }

    [Fact]
    public async Task Get_IdIsNotNullButNotExistsInCollection_ShouldReturnNull()
    {
        var id = new Guid("2D6D48A8-F8A3-4496-A16C-9D3E89CC2283");
        var expectedAppointment = await _appointmentsGetterService.Get(id);
        expectedAppointment.Should().BeNull();
    }

    [Fact]
    public async Task Get_IdIsNotNullAndExistsInCollection_ShouldReturnMatchingAppointment()
    {
        var appointment = _fixture.Build<Appointment>()
            .With(appointment => appointment.State, State.Finished.ToString())
            .Create();

        _mockAppointmentsRepository
            .Setup(method => method.Get(It.IsAny<Guid>()))
            .ReturnsAsync(appointment);

        var expectedAppointment = await _appointmentsGetterService.Get(appointment.Id);
        expectedAppointment.Should().Be(appointment.ToAppointmentResponse());
    }
    #endregion

  
    
}