using Exam.App.Domain;

namespace Exam.App.Dtos
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string PetName { get; set; }
        public string AnymalType { get; set; }
        public string OwnerName { get; set; }
        public string VeterinarianName { get; set; }

        public DateTime DateOfBirth { get; set; }

        //public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public int Age =>
        DateTime.Today.Year - DateOfBirth.Year -
        (DateTime.Today < DateOfBirth.AddYears(DateTime.Today.Year - DateOfBirth.Year) ? 1 : 0);
        

    }
}
