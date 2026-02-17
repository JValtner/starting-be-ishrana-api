using System.Text.Json.Serialization;

namespace Exam.App.Domain
{
    public class MedicalReport
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        [JsonIgnore]
        public Appointment? Appointment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Anamneza { get; set; }
        public double Weight { get; set; }
    }
}
