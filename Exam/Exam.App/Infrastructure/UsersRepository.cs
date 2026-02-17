using Exam.App.Domain;
using Microsoft.AspNetCore.Identity;

namespace Exam.App.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllByRoleAsync(string role)
        {
            return (await _userManager.GetUsersInRoleAsync(role)).ToList();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
