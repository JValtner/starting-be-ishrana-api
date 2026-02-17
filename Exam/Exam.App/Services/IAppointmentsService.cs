using Exam.App.Domain;
using Exam.App.Dtos;

namespace Exam.App.Services
{
    public interface IAppointmentsService
    {
        Task<List<AppointmentDTO>> GetAllByVetAsync(string vetId);
        Task<Appointment> MakeAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(int id, Appointment appointment);
        Task<Appointment> CancelAppointmentAsync(int id, string cancelationReason);
    }
}