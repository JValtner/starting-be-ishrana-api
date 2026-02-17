using Exam.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Veterinarian,Assistant")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("by-role/{role}")]
        public async Task<IActionResult> GetAllByRoleAsync(string role)
        {
            return Ok(await _usersService.GetAllUsersByRole(role));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return Ok(await _usersService.GetUserByIdAsync(id));
        }

    }
}
