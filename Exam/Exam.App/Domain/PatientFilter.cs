namespace Exam.App.Domain;

public class PatientFilter
{
    public string? PetName { get; set; }
    public string? VeterinarianId { get; set; }
    public int? AnimalTypeId { get; set; }
    public int? AgeFrom { get; set; }
    public int? AgeTo { get; set; }  

}
