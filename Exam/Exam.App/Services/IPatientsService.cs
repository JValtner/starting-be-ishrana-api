using Exam.App.Domain;
using Exam.App.Dtos;

namespace Exam.App.Services
{
    public interface IPatientsService
    {
        Task<Patient?> AddAsync(Patient? patient);
        Task  DeleteAsync(int id);
        Task<List<Patient>> GetAllAsync();
        Task<List<PatientDTO>> GetAllFilteredAsync(PatientFilter filter);
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient?> UpdateAsync(int id, Patient patient);
    }
}