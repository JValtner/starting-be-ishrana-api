using Exam.App.Domain;

namespace Exam.App.Services
{
    public interface IAnimalsService
    {
        Task<List<AnimalType>> GetAllAsync();
    }
}