using Exam.App.Domain;
using Exam.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Veterinarian,Assistant")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _patientsService.GetAllAsync());
        }
        [HttpGet ("filtered")]
        public async Task<IActionResult> GetAllFilteredAsync([FromQuery] PatientFilter filter)
        {
            return Ok(await _patientsService.GetAllFilteredAsync(filter));
        }
        //GET/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _patientsService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Patient patient) 
        { 
            return Ok(await _patientsService.AddAsync(patient)); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Patient patient)
        {
            return Ok(await _patientsService.UpdateAsync(id, patient));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _patientsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
