using Exam.App.Domain;
using Microsoft.AspNetCore.Mvc;
using Exam.App.Middleware.Exceptions;

namespace Exam.App.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<ApplicationUser>> GetAllUsersByRole(string role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            var userList = await _usersRepository.GetAllByRoleAsync(role);
            return userList;
        }
        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NotFoundException(0);
            }

            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException(0);
            }

            return user;
        }

    }
}
