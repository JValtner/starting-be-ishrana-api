namespace Exam.App.Domain
{
    public class AnimalType
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
}
