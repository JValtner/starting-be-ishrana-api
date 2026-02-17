using Exam.App.Domain;
using Exam.App.Middleware.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Exam.App.Dtos;

namespace Exam.App.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IAppointmentsRepository _appointmentsRepository;

        public AppointmentsService(IAppointmentsRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;
        }

        public async Task<List<AppointmentDTO>> GetAllByVetAsync(string vetId)
        {
            List<Appointment> appointments = await _appointmentsRepository.GetAllByVetAsync(vetId);
            List<AppointmentDTO> appointmentsDTO =appointments
                .OrderBy(a => a.StartDate)
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,

                    Date = DateOnly.FromDateTime(a.StartDate),
                    StartTime = TimeOnly.FromDateTime(a.StartDate),
                    EndTime = TimeOnly.FromDateTime(a.EndDate),

                    PatientName = a.Patient!.PetName,
                    OwnerName = a.Patient!.Owner!.Name,

                    Status = a.Status,
                    CancelationReason = a.CancelationReason,
                    Report = a.Report
                })
                .ToList();

            return (appointmentsDTO);
        }
        public async Task<Appointment> MakeAppointmentAsync (Appointment appointment)
        {
            if (appointment == null)
            {
                throw new BadRequestException("Appointment doesnt exist");
            }

            if (appointment.EndDate <= appointment.StartDate)
            {
                throw new BadRequestException("End time must be after start time");
            }

            if (!await _appointmentsRepository.CheckAvailability(appointment.VetId, appointment.StartDate, appointment.EndDate, null))
            {
                throw new BadRequestException("Doctor already has an appointment");
            }

            return (await _appointmentsRepository.AddAsync(appointment));
        }

        public async Task<Appointment> UpdateAppointmentAsync(int id, Appointment appointment)
        {
            if (appointment == null)
            {
                throw new BadRequestException("Appointment doesnt exist");
            }

            if (appointment.EndDate <= appointment.StartDate)
            {
                throw new BadRequestException("End time must be after start time");
            }

            
            if (id != appointment.Id)
            {
                throw new BadRequestException("id mismath");
            }

            if (!await _appointmentsRepository.CheckAvailability(appointment.VetId, appointment.StartDate, appointment.EndDate, appointment.Id))
            {
                throw new BadRequestException("Doctor already has an appointment");
            }
            return (await _appointmentsRepository.UpdateAsync(appointment));
        }
        public async Task<Appointment> CancelAppointmentAsync(int id, string cancelationReason)
        {
            if (cancelationReason.IsNullOrEmpty())
            {
                throw new BadRequestException("No data");
            }
            if (id ==null)
            {
                throw new BadRequestException("id mising");
            }

            return (await _appointmentsRepository.CancelAsync(id, cancelationReason));
        }

    }
}
