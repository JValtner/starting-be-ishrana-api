using Exam.App.Domain;

namespace Exam.App.Services
{
    public interface IReportsService
    {
        Task<MedicalReport> MakeReportAsync(MedicalReport report);
        Task<MedicalReport> UpdateReportAsync(int reportId, MedicalReport report);
    }
}