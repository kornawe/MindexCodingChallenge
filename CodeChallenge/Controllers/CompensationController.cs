using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compensationService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compensationService = compensationService;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            // Make sure the compensation that was sent to us was good.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCompensation = _compensationService.Create(compensation);

            if (newCompensation == null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("getCompensationByEmployeeId", new { employeeId = compensation.EmployeeId }, newCompensation);
        }

        [HttpGet("{employeeId}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(String employeeId)
        {
            _logger.LogDebug($"Received compensation get request for '{employeeId}'");

            var compensation = _compensationService.GetByEmployeeId(employeeId);

            if (compensation == null || string.IsNullOrEmpty(compensation.EmployeeId))
            {
                return NotFound();
            }

            return Ok(compensation);
        }
    }
}
