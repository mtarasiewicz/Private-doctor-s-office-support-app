using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.RepositoryContracts;
using DocHub.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DocHub.Infrastructure.Repositories
{
    public class PatientsRepository : IPatientsRepository
    { 
        private readonly ApplicationDbContext _dbContext;
        public PatientsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Patient> Add(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> Get(Guid? id) 
            => await _dbContext.Patients.FindAsync(id);

        public async Task<List<Patient>?> GetAll()
            => await _dbContext.Patients.ToListAsync();

        public async Task<Patient?> GetByUserId(Guid? userId) 
            => await _dbContext.Patients.Where(x => x.UserId == userId).FirstOrDefaultAsync();

        
       
        public async Task<Patient> Update(Patient patient)
        {
            Patient? matchingPatient = await _dbContext.Patients.FirstOrDefaultAsync(tmp => tmp.Id == patient.Id);
            if (matchingPatient is null) return patient;

            matchingPatient.FirstName = patient.FirstName;
            matchingPatient.LastName = patient.LastName;
            matchingPatient.Email = patient.Email;
            matchingPatient.Address = patient.Address;
            matchingPatient.City = patient.City;
            matchingPatient.Allergies = patient.Allergies;
            matchingPatient.PostalCode = patient.PostalCode;
            matchingPatient.PeselNumber = patient.PeselNumber;
            matchingPatient.HistoryOfDiseases = patient.HistoryOfDiseases;
            matchingPatient.TakenMedications = patient.TakenMedications;
            matchingPatient.PhoneNumber = patient.PhoneNumber;
            matchingPatient.DateOfBirth = patient.DateOfBirth;
            _dbContext.Update(matchingPatient);
            await _dbContext.SaveChangesAsync();
            return matchingPatient;
        }
    }
}
