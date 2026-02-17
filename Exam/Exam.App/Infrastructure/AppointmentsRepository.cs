using Exam.App.Domain;
using Exam.App.Middleware.Exceptions;
using Exam.App.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Repository
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private AppDbContext _context;

        public AppointmentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            List<Appointment> appointments = new List<Appointment>();
            appointments = await _context.Appointments
                .ToListAsync();
            return appointments;
        }
        public async Task<List<Appointment>> GetAllByVetAsync(string vetId)
        {
            return await _context.Appointments
                .AsNoTracking()

                .Include(a => a.Patient)
                    .ThenInclude(p => p.Owner)

                .Include(a => a.Report)

                .Where(a => a.VetId == vetId)
                .OrderBy(a => a.StartDate.Date)
                .ThenBy(a => a.StartDate)

                .ToListAsync();
        }




        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Appointments.AnyAsync(b => b.Id == id);
        }
        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<Appointment> UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<Appointment> CancelAsync(int id, string cancRes)
        {

            var app = await GetByIdAsync(id);
            if (app == null)
            {
                throw new BadRequestException("not found");
            }
            app.Status = AppointmentStatus.Cancelled;
            app.CancelationReason = cancRes;
            _context.Appointments.Update(app);
            await _context.SaveChangesAsync();
            return app;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return false;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> CheckAvailability(
            string vetId,
            DateTime newStart,
            DateTime newEnd,
            int? excludeAppointmentId = null)
                {
                    return !await _context.Appointments.AnyAsync(a =>
                        a.VetId == vetId &&
                        a.Status == AppointmentStatus.Scheduled &&
                        (excludeAppointmentId == null || a.Id != excludeAppointmentId) &&
                        newStart < a.EndDate &&
                        newEnd > a.StartDate
                    );
                }


    }
}
