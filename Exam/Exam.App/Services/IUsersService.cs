using Exam.App.Domain;

namespace Exam.App.Services
{
    public interface IUsersService
    {
        Task<List<ApplicationUser>> GetAllUsersByRole(string role);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
    }
}