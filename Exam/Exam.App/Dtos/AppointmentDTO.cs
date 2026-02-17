using Exam.App.Domain;

namespace Exam.App.Dtos
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string OwnerName { get; set; }
        public string PatientName { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public string? CancelationReason { get; set; }
        public MedicalReport? Report { get; set; }
    }
}
