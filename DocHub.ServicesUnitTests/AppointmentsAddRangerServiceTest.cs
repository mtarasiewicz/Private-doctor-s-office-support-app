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

public class AppointmentsAddRangerServiceTest
{
    private readonly Mock<IAppointmentsRepository> _mockAppointmentsRepository;
    private readonly IAppointmentsRepository _appointmentsRepository;
    private readonly IAppointmentsAddRangeService _appointmentsAddRangeService;
    private readonly IFixture _fixture;

    public AppointmentsAddRangerServiceTest()
    {
        _mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
        _appointmentsRepository = _mockAppointmentsRepository.Object;
        _appointmentsAddRangeService = new AppointmentsAddRangeService(_appointmentsRepository);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task AddRange_RequestDatesAreNull_ShouldThrowArgumentNullException()
    {
        var request = _fixture.Build<AppointmentAddRangeRequest>()
            .With(request => request.StartDate, null as DateTime?)
            .With(request => request.EndDate, null as DateTime?)
            .Create();
        Func<Task> action = async () => await _appointmentsAddRangeService.AddRange(request);
        await action.Should().ThrowAsync<ArgumentNullException>();
    }
}