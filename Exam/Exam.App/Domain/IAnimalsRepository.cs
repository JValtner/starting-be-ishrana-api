namespace Exam.App.Domain
{
    public interface IAnimalsRepository
    {
        Task<List<AnimalType>> GetAllAsync();
    }
}