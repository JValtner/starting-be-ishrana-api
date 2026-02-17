using Exam.App.Domain;
using Exam.App.Infrastructure.Database;
using Exam.App.Middleware.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure
{
    public class ReportsRepository : IReportsRepository
    {
        private AppDbContext _context;

        public ReportsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<MedicalReport> AddAsync(MedicalReport report)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Report)
                .FirstOrDefaultAsync(a => a.Id == report.AppointmentId);

            if (appointment == null)
                throw new BadRequestException("Appointment not found");

            if (appointment.Report != null)
                throw new BadRequestException(
                    "Medical report already exists for this appointment"
                    //go to update ???
                );

            appointment.Status = AppointmentStatus.Completed;

            report.CreatedAt = DateTime.UtcNow;
            report.AppointmentId = appointment.Id;

            _context.MedicalReports.Add(report);
            await _context.SaveChangesAsync();

            return report;
        }


        public async Task<MedicalReport> UpdateAsync(int reportId, MedicalReport incoming)
                {
                    var existing = await _context.MedicalReports
                        .FirstOrDefaultAsync(r => r.Id == reportId);

                    if (existing == null)
                        throw new BadRequestException("Medical report not found");

                    existing.Weight = incoming.Weight;
                    existing.Anamneza = incoming.Anamneza;

                    await _context.SaveChangesAsync();
                    return existing;
                }

    }
}
