using System.Data.Common;
using AutoFixture;
using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Core.DTO;
using DocHub.Core.ServiceContracts;
using DocHub.Core.Services;
using FluentAssertions;
using Moq;

namespace DocHub.PatientsServiceTests;

public class PatientsGetterServiceTest
{
    private readonly IPatientsGetterService _patientsGetterService;
    private readonly IFixture _fixture;
    private readonly IPatientsRepository _patientsRepository;
    private readonly Mock<IPatientsRepository> _mockPatientsRepository;

    public PatientsGetterServiceTest()
    {
        _mockPatientsRepository = new();
        _patientsRepository = _mockPatientsRepository.Object;
        _patientsGetterService = new PatientsGetterService(_patientsRepository);
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    #region Get()

    /// <summary>
    /// Tests whether <see cref="PatientsGetterService.Get(Guid?)"/>
    /// method of <see cref="PatientsGetterService"/>
    /// returns null when null is provided as a method parameter (id)    
    /// </summary>
    [Fact]
    public async Task Get_IdIsNull_ShouldReturnNull()
    {
        Guid? id = null;
        PatientResponse? patientResponse = await _patientsGetterService.Get(id);
        patientResponse.Should().BeNull();
    }

    /// <summary>
    /// Tests whether Get method returns null when it does not find a matching value in the repository
    /// </summary>
    [Fact]
    public async Task Get_IdIsNotNullButNotExistsInCollection_ShouldReturnNull()
    {
        var nonExistingId = new Guid("B9476717-C19D-4F53-8F39-760BBA70F386");
        var patientResponse = await _patientsGetterService.Get(nonExistingId);
        patientResponse.Should().BeNull();
    }

    /// <summary>
    /// Tests whether the get service method will return a matching object when providing an existing Id
    /// </summary>
    [Fact]
    public async Task Get_IdIsNotNullAndExistsInRepository_ShouldReturnMatchingPatient()
    {
        var patient = _fixture.Build<Patient>()
            .With(patient => patient.FirstName, "Jan")
            .With(patient => patient.LastName, "Kowalski")
            .With(patient => patient.Email, "somebody@nowhere.com")
            .Create<Patient>();

        var expectedPatient = patient.ToPatientResponse();
        _mockPatientsRepository.Setup(method => method.Get(It.IsAny<Guid>())).ReturnsAsync(patient);

        var returnedPatient = await _patientsGetterService.Get(patient.Id);

        returnedPatient.Should().Be(expectedPatient);
    }

    #endregion

    #region GetByUserId()

    /// <summary>
    /// Tests whether the GetByUserId method will return null if the id given in the parameter is null
    /// </summary>
    [Fact]
    public async Task GetByUserId_IdIsNull_ShouldReturnNull()
    {
        Guid? id = null;
        var patientResponse = await _patientsGetterService.GetByUserId(id);
        patientResponse.Should().BeNull();
    }

    /// <summary>
    /// Tests whether the GetByUserId method will return null if it does not find a matching object
    /// </summary>
    [Fact]
    public async Task GetByUserId_IdIsNotNullButNotExistsInCollection_ShouldReturnNull()
    {
        var id = new Guid("EB863FDA-3809-4E24-A5B1-C5552F2153CD");
        var patientResponse = await _patientsGetterService.GetByUserId(id);
        patientResponse.Should().BeNull();
    }

    /// <summary>
    /// Tests whether the GetByUserId method will return a matching object if the id that is in the collection is provided
    /// </summary>
    [Fact]
    public async Task GetByUserId_IdIsNotNullAndIdExistsInCollection_ShouldReturnMatchingPatient()
    {
        var patient = _fixture.Build<Patient>().Create();
        var expectedPatient = patient.ToPatientResponse();
        _mockPatientsRepository.Setup(method => method.GetByUserId(It.IsAny<Guid>())).ReturnsAsync(patient);
        var returnedPatient = await _patientsGetterService.GetByUserId(patient.Id);
        returnedPatient.Should().Be(expectedPatient);
    }

    #endregion

    #region GetAll()

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public async Task GetAll_CollectionIsEmpty_ShouldReturnTrue()
    {
        _mockPatientsRepository
            .Setup(method => method.GetAll())
            .ReturnsAsync(new List<Patient>());
        var patients = await _patientsGetterService.GetAll();
        patients.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_CollectionWithFewRecords_ShouldReturnCollectionOfPatients()
    {
        var patients = new List<Patient>();
        for (var i = 0; i < 10; i++)
            patients.Add(_fixture.Build<Patient>().Create());
        _mockPatientsRepository
            .Setup(method => method.GetAll())
            .ReturnsAsync(patients);
        var returnedPatients = await _patientsGetterService.GetAll();
        returnedPatients.Should().BeEquivalentTo(returnedPatients);
    }

    #endregion
}