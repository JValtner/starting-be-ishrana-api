namespace Exam.App.Domain
{
    public class Patient
    {
        public int Id {  get; set; }
        public string PetName { get; set; }
        public int AnimalTypeId { get; set; }
        public AnimalType? AnimalType { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser? Owner{  get; set; }
        public string VeterinarianId { get; set; }
        public ApplicationUser? Veterinarian { get; set; }

        public DateTime DateOfBirth { get; set; }
    
        public ICollection<Appointment> Appointments { get; set; }=new List<Appointment>();
        public int Age =>
        DateTime.Today.Year - DateOfBirth.Year -
        (DateTime.Today < DateOfBirth.AddYears(DateTime.Today.Year - DateOfBirth.Year) ? 1 : 0);

    }
}
