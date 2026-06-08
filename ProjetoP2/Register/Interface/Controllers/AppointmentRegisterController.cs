using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Register.Application.DTOs.AppointmentRegister;
using ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases;

namespace ProjetoP2.Register.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentRegisterController : ControllerBase
    {
        private readonly CreateAppointmentRegisterUseCase _createUseCase;
        private readonly GetAllAppointmentRegisterUseCase _getAllUseCase;
        private readonly GetByIdAppointmentRegisterUseCase _getByIdUseCase;
        private readonly UpdateAppointmentRegisterUseCase _updateUseCase;
        private readonly DeleteAppointmentRegisterUseCase _deleteUseCase;

        public AppointmentRegisterController(
            CreateAppointmentRegisterUseCase createUseCase,
            GetAllAppointmentRegisterUseCase getAllUseCase,
            GetByIdAppointmentRegisterUseCase getByIdUseCase,
            UpdateAppointmentRegisterUseCase updateUseCase,
            DeleteAppointmentRegisterUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentRegisterDto dto)
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
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentRegisterDto dto)
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
