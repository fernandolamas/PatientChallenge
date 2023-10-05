using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientChallenge.Service.PatientService;
using PatientChallenge.Shared.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace PatientChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost(Name = "CreatePatient")]
        [SwaggerOperation("Create a patient")]
        [SwaggerResponse(StatusCodes.Status200OK, "Patient created",typeof(Patient))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Invalid data provided")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Patient already exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error encountered")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> CreatePatient(Patient patient)
        {
            try
            {
                long patientId = await _patientService.CreatePatient(patient);
                patient.Id = patientId;
                return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
            }
            catch (ArgumentException arEx)
            {
                return BadRequest(arEx.Message);
            }
            catch (DuplicateNameException dEx)
            {
                return Conflict(dEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet(Name = "GetPatient")]
        [SwaggerOperation("Find a patient")]
        [SwaggerResponse(StatusCodes.Status200OK, "Patient exists", typeof(Patient))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> GetPatient(long id)
        {
            Patient? patient = await _patientService.GetPatient(id);
            return patient != null ? Ok(patient) : NotFound("Not found");
        }
        [HttpPut(Name = "UpdatePatient")]
        [SwaggerOperation("Update a patient")]
        [SwaggerResponse(StatusCodes.Status200OK, "Patient updated", typeof(Patient))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data provided")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "Patient already exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Server error encountered")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> UpdatePatient(Patient patient)
        {
            try
            {
                Patient? modifiedPatient = await _patientService.UpdatePatient(patient);
                return modifiedPatient == null ? NotFound("Not found") : Ok(modifiedPatient);
            }
            catch (ArgumentException arEx)
            {
                return BadRequest(arEx.Message);
            }
            catch (DuplicateNameException dEx)
            {
                return Conflict(dEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete(Name = "DeletePatient")]
        [SwaggerOperation("Delete a patient")]
        [SwaggerResponse(StatusCodes.Status200OK, "Patient deleted")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult> DeletePatient(long id)
        {
            return await _patientService.DeletePatient(id) ? Ok() : NotFound("Not found");
        }

    }
}

