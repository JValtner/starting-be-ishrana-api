using AutoMapper;
using Exam.App.Domain;
using Exam.App.Middleware.Exceptions;
using Exam.App.Dtos;


namespace Exam.App.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientsService> _logger;

        public PatientsService(
            IPatientsRepository patientsRepository,
            IMapper mapper,
            ILogger<PatientsService> logger)
        {
            _patientsRepository = patientsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _patientsRepository.GetAllAsync();
        }
        public async Task<List<PatientDTO>> GetAllFilteredAsync(PatientFilter filter)
        {
            List<Patient> patients = await _patientsRepository.GetAllFilteredAsync(filter);

            return _mapper.Map<List<PatientDTO>>(patients);
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            Patient? patient = await _patientsRepository.GetByIdAsync(id);
            if (patient == null)
            {
                throw new NotFoundException(id);
            }
            return patient;

        }

        public async Task<Patient?> AddAsync(Patient? patient)
        {
            if (patient == null)
            {
                throw new BadRequestException("No data");
            }
            patient.DateOfBirth = DateTime.SpecifyKind(
                patient.DateOfBirth,
                DateTimeKind.Utc
            );
            return await _patientsRepository.AddAsync(patient);
        }

        public async Task<Patient> UpdateAsync(int id, Patient patient)
        {
            var existing = await _patientsRepository.GetByIdAsync(id);
            if (existing == null)
                throw new NotFoundException(id);

            existing.PetName = patient.PetName;
            existing.AnimalTypeId = patient.AnimalTypeId;
            existing.OwnerId = patient.OwnerId;
            existing.VeterinarianId = patient.VeterinarianId;

            existing.DateOfBirth = DateTime.SpecifyKind(
                patient.DateOfBirth,
                DateTimeKind.Utc
            );

            await _patientsRepository.UpdateAsync(existing);
            return existing;
        }



        public async Task DeleteAsync(int id)
        {
            if (!await _patientsRepository.ExistsAsync(id))
                throw new NotFoundException(id);

            await _patientsRepository.DeleteAsync(id);
        }
    }
}
