using Exam.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Veterinarian,Assistant")]
    public class AnimalsController : Controller
    {
        private readonly IAnimalsService _animalsService;

        public AnimalsController(IAnimalsService animalsService)
        {
            _animalsService = animalsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _animalsService.GetAllAsync());
        }

    }
}
