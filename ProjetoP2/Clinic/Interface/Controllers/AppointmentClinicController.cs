using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Application.UseCases;
using ProjetoP2.Clinic.Application.UseCases.Appointment;

namespace ProjetoP2.Register.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentClinicController : ControllerBase
    {
        private readonly CreateAppointmentClinicUseCase _createUseCase;
        private readonly GetAllAppointmentClinicUseCase _getUseCase;
        private readonly DeleteAppointmentClinicUseCase _deleteUseCase;

        public AppointmentClinicController(
            CreateAppointmentClinicUseCase createUseCase, 
            GetAllAppointmentClinicUseCase getUseCase, 
            DeleteAppointmentClinicUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public IActionResult Create(CreateAppointmentClinicDto dto)
        {
            var result = _createUseCase.Run(dto);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(Guid Id)
        {
            return Ok(_getUseCase.Run());
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            _deleteUseCase.Run(Id);
            return NoContent();
        }
    }
}
