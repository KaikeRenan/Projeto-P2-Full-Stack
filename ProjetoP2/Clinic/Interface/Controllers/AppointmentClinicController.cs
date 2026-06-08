using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Clinic.Application.DTOs.Appointment;
using ProjetoP2.Clinic.Application.UseCases.Appointment;

namespace ProjetoP2.Clinic.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentClinicController : ControllerBase
    {
        private readonly CreateAppointmentClinicUseCase _createUseCase;
        private readonly GetAllAppointmentClinicUseCase _getAllUseCase;
        private readonly GetByIdAppointmentClinicUseCase _getByIdUseCase;
        private readonly UpdateAppointmentClinicUseCase _updateUseCase;
        private readonly DeleteAppointmentClinicUseCase _deleteUseCase;

        public AppointmentClinicController(
            CreateAppointmentClinicUseCase createUseCase,
            GetAllAppointmentClinicUseCase getAllUseCase,
            GetByIdAppointmentClinicUseCase getByIdUseCase,
            UpdateAppointmentClinicUseCase updateUseCase,
            DeleteAppointmentClinicUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentClinicDto dto)
        {
            var result = await _createUseCase.Run(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllUseCase.Run();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _getByIdUseCase.Run(id);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentClinicDto dto)
        {
            var result = await _updateUseCase.Run(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteUseCase.Run(id);
            return NoContent();
        }
    }
}
