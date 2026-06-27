using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using BLL.Services.Interfaces;

namespace HRMSAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _logger = logger;
        }

        /// <summary>
        /// Get a registration by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationDto>> GetRegistrationById(int id)
        {
            try
            {
                var registration = await _registrationService.GetRegistrationByIdAsync(id);
                if (registration == null)
                    return NotFound(new { message = $"Registration with ID {id} not found." });

                return Ok(registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving registration by ID");
                return StatusCode(500, new { message = "An error occurred while retrieving the registration." });
            }
        }

        /// <summary>
        /// Get all registrations
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetAllRegistrations()
        {
            try
            {
                var registrations = await _registrationService.GetAllRegistrationsAsync();
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all registrations");
                return StatusCode(500, new { message = "An error occurred while retrieving registrations." });
            }
        }

        /// <summary>
        /// Get active registrations only
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<RegistrationDto>>> GetActiveRegistrations()
        {
            try
            {
                var registrations = await _registrationService.GetActiveRegistrationsAsync();
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active registrations");
                return StatusCode(500, new { message = "An error occurred while retrieving active registrations." });
            }
        }

        /// <summary>
        /// Get registration by email
        /// </summary>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<RegistrationDto>> GetRegistrationByEmail(string email)
        {
            try
            {
                var registration = await _registrationService.GetRegistrationByEmailAsync(email);
                if (registration == null)
                    return NotFound(new { message = $"Registration with email {email} not found." });

                return Ok(registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving registration by email");
                return StatusCode(500, new { message = "An error occurred while retrieving the registration." });
            }
        }

        /// <summary>
        /// Create a new registration
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RegistrationDto>> CreateRegistration([FromBody] CreateRegistrationDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var registration = await _registrationService.CreateRegistrationAsync(createDto);
                return CreatedAtAction(nameof(GetRegistrationById), new { id = registration.Id }, registration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating registration");
                return StatusCode(500, new { message = "An error occurred while creating the registration." });
            }
        }

        /// <summary>
        /// Update an existing registration
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistration(int id, [FromBody] UpdateRegistrationDto updateDto)
        {
            try
            {
                if (id != updateDto.Id)
                    return BadRequest(new { message = "ID mismatch" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _registrationService.UpdateRegistrationAsync(updateDto);
                if (!result)
                    return NotFound(new { message = $"Registration with ID {id} not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating registration");
                return StatusCode(500, new { message = "An error occurred while updating the registration." });
            }
        }

        /// <summary>
        /// Delete a registration
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            try
            {
                var result = await _registrationService.DeleteRegistrationAsync(id);
                if (!result)
                    return NotFound(new { message = $"Registration with ID {id} not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting registration");
                return StatusCode(500, new { message = "An error occurred while deleting the registration." });
            }
        }
    }
}
