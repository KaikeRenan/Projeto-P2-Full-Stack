using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Application.UseCases.PetUseCases;

namespace ProjetoP2.Register.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetRegisterController : ControllerBase
    {
        private readonly CreatePetRegisterUseCase _createUseCase;
        private readonly GetAllPetRegisterUseCase _getAllUseCase;
        private readonly GetByIdPetRegisterUseCase _getByIdUseCase;
        private readonly UpdatePetRegisterUseCase _updateUseCase;
        private readonly DeletePetRegisterUseCase _deleteUseCase;

        public PetRegisterController(
            CreatePetRegisterUseCase createUseCase,
            GetAllPetRegisterUseCase getAllUseCase,
            GetByIdPetRegisterUseCase getByIdUseCase,
            UpdatePetRegisterUseCase updateUseCase,
            DeletePetRegisterUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetRegisterDto dto)
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
        public async Task<IActionResult> Update([FromBody] UpdatePetRegisterDto dto)
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