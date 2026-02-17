using Exam.App.Domain;
using Exam.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Veterinarian,Assistant")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        [HttpGet("byVet/{vetId}")]
        public async Task<IActionResult> GetAllByVetAsync([FromRoute] string vetId)
        {
            return Ok(await _appointmentsService.GetAllByVetAsync(vetId));
        }

        [HttpPost]
        public async Task<IActionResult> MakeAppointmentAsync([FromBody] Appointment appointment)
        {
            return Ok(await _appointmentsService.MakeAppointmentAsync(appointment));
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateAppointmentAsync(int id, [FromBody] Appointment appointment)
        {
            return Ok(await _appointmentsService.UpdateAppointmentAsync(id, appointment));
        }
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> UpdateAppointmentAsync(int id, [FromBody] string cancelationReason)
        {
            return Ok(await _appointmentsService.CancelAppointmentAsync(id, cancelationReason));
        }


    }
}
