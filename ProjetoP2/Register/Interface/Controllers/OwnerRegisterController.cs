using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;

namespace ProjetoP2.Register.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerRegisterController : ControllerBase
    {
        private readonly CreateOwnerRegisterUseCase _createUseCase;
        private readonly GetAllOwnerRegisterUseCase _getAllUseCase;
        private readonly GetByIdOwnerRegisterUseCase _getByIdUseCase;
        private readonly UpdateOwnerRegisterUseCase _updateUseCase;
        private readonly DeleteOwnerRegisterUseCase _deleteUseCase;

        public OwnerRegisterController(
            CreateOwnerRegisterUseCase createUseCase,
            GetAllOwnerRegisterUseCase getAllUseCase,
            GetByIdOwnerRegisterUseCase getByIdUseCase,
            UpdateOwnerRegisterUseCase updateUseCase,
            DeleteOwnerRegisterUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerRegisterDto dto)
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
        public async Task<IActionResult> Update([FromBody] UpdateOwnerRegisterDto dto)
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