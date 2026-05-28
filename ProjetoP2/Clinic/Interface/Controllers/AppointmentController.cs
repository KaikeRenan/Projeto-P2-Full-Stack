using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Application.UseCases;
using ProjetoP2.Clinic.Application.UseCases.CreateAppointmentUseCase;
using ProjetoP2.Clinic.Application.UseCases.DeleteAppointmentUseCase;
using ProjetoP2.Clinic.Application.UseCases.GetAppointmentUseCase;

namespace ProjetoP2.Register.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly CreateAppointmentUseCase _createUseCase;
        private readonly GetAppointmentUseCase _getUseCase;
        private readonly DeleteAppointmentUseCase _deleteUseCase;

        public AppointmentController(CreateAppointmentUseCase createUseCase, GetAppointmentUseCase getUseCase, DeleteAppointmentUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public IActionResult Create(CreateAppointmentDto dto)
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
