namespace Exam.App.Domain
{
    public interface IPatientsRepository
    {
        Task<Patient> AddAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Patient>> GetAllAsync();
        Task<List<Patient>> GetAllFilteredAsync(PatientFilter filter);
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> UpdateAsync(Patient patient);
    }
}