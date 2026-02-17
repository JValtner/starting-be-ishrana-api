using Exam.App.Domain;
using Exam.App.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Repository
{
    public class PatientsRepository : IPatientsRepository
    {
        private AppDbContext _context;

        public PatientsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            List<Patient> patients = new List<Patient>();
            patients = await _context.Patients
                .ToListAsync();
            return patients;
        }
        public async Task<List<Patient>> GetAllFilteredAsync(PatientFilter filter)
        {
            var query = _context.Patients
                .Include(p => p.AnimalType)
                .Include(p => p.Owner)
                .Include(p => p.Veterinarian)
                //.Include(p => p.Appointments)
                .AsNoTracking();

            // Apply filters
            query = FilterPatientsAsync(query, filter);
             
            var filteredPatients = await query.ToListAsync();

            return filteredPatients;
        }
        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Patients.AnyAsync(b => b.Id == id);
        }
        public async Task<Patient> AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<Patient> UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Patient patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        private static IQueryable<Patient> FilterPatientsAsync(
    IQueryable<Patient> patients,
    PatientFilter filter)
{
    // Pet name (text search)
    if (!string.IsNullOrWhiteSpace(filter.PetName))
    {
        patients = patients.Where(p =>
            p.PetName.ToLower().Contains(filter.PetName.ToLower()));
    }

    // Veterinarian (GUID string → exact match)
    if (!string.IsNullOrWhiteSpace(filter.VeterinarianId))
    {
        patients = patients.Where(p =>
            p.VeterinarianId == filter.VeterinarianId);
    }

    // Animal type
    if (filter.AnimalTypeId.HasValue)
    {
        patients = patients.Where(p =>
            p.AnimalTypeId == filter.AnimalTypeId.Value);
    }

    // Age filtering (derived from DateOfBirth)
    if (filter.AgeFrom.HasValue || filter.AgeTo.HasValue)
    {
        var today = DateTime.UtcNow.Date;

        if (filter.AgeFrom.HasValue)
        {
            var maxDob = today.AddYears(-filter.AgeFrom.Value);
            patients = patients.Where(p => p.DateOfBirth <= maxDob);
        }

        if (filter.AgeTo.HasValue)
        {
            var minDob = today.AddYears(-filter.AgeTo.Value);
            patients = patients.Where(p => p.DateOfBirth >= minDob);
        }
    }

    return patients;
}

    }
}
