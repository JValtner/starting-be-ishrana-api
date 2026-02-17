namespace Exam.App.Domain
{
    public interface IAppointmentsRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<List<Appointment>> GetAllAsync();
        Task<List<Appointment>> GetAllByVetAsync(string vetId);
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> UpdateAsync(Appointment appointment);
        Task<Appointment> CancelAsync(int id, string cancRes);
        Task<bool> CheckAvailability(string vetId, DateTime newStart, DateTime newEnd, int? excludeAppointmentId = null);
    }
}