using Microsoft.AspNetCore.Identity;

namespace Exam.App.Domain;

public class ApplicationUser : IdentityUser
{
    public required string Name { get; set; }
    public required string Surname { get; set; }

    //Navigation
    public ICollection<Patient> AssignedPatients { get; set; }=new List<Patient>();
    public ICollection<Appointment> Appointments { get; set; } =new List<Appointment>();

    public ICollection<Patient> OwnedPatients { get; set; } = new List<Patient>();
}