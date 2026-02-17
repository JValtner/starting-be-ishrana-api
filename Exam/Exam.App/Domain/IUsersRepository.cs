namespace Exam.App.Domain
{
    public interface IUsersRepository
    {
        Task<List<ApplicationUser>> GetAllByRoleAsync(string role);
        Task<ApplicationUser?> GetByIdAsync(string id);
    }
}