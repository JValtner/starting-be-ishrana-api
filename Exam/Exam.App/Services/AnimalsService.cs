using Exam.App.Domain;

namespace Exam.App.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;

        public AnimalsService(IAnimalsRepository animalsRepository)
        {
            _animalsRepository = animalsRepository;
        }

        public async Task<List<AnimalType>> GetAllAsync()
        {

            return (await _animalsRepository.GetAllAsync()).ToList();
        }


    }
}
