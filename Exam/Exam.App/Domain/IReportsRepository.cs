namespace Exam.App.Domain
{
    public interface IReportsRepository
    {
        Task<MedicalReport> AddAsync(MedicalReport report);
        Task<MedicalReport> UpdateAsync(int reportId, MedicalReport incoming);
    }
}