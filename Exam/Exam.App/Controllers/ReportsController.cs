using Exam.App.Domain;
using Exam.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Veterinarian,Assistant")]
    public class ReportsController : Controller
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateReport([FromBody] MedicalReport report)
        {
            return Ok(await _reportsService.MakeReportAsync(report));
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] MedicalReport report)
        {
            return Ok(await _reportsService.UpdateReportAsync(id, report));
        }

    }
}
