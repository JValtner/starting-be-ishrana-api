using Exam.App.Domain;
using Exam.App.Middleware.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Exam.App.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _reportsRepository;

        public ReportsService(IReportsRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;
        }

        
        public async Task<MedicalReport> MakeReportAsync(MedicalReport report)
        {
            if (report == null)
            {
                throw new BadRequestException("Report doesnt exist");
            }


            return (await _reportsRepository.AddAsync(report));
        }

        public async Task<MedicalReport> UpdateReportAsync(int reportId, MedicalReport report)
        {
            if (report == null)
                throw new BadRequestException("Report data missing");

            return await _reportsRepository.UpdateAsync(reportId, report);
        }
    }
}
