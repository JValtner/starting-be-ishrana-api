namespace Exam.App.Domain
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string VetId { get; set; }
        public ApplicationUser? ChosenVet { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient{ get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

        public string? CancelationReason { get; set; }
        public MedicalReport? Report { get; set; }

    }
}
