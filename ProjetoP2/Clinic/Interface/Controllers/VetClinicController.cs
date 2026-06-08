using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Clinic.Application.DTOs.Vet;
using ProjetoP2.Clinic.Application.UseCases.Vet;

namespace ProjetoP2.Clinic.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VetClinicController : ControllerBase
    {
        private readonly CreateVetClinicUseCase _createUseCase;
        private readonly GetAllVetClinicUseCase _getAllUseCase;
        private readonly GetByIdVetClinicUseCase _getByIdUseCase;
        private readonly UpdateVetClinicUseCase _updateUseCase;
        private readonly DeleteVetClinicUseCase _deleteUseCase;

        public VetClinicController(
            CreateVetClinicUseCase createUseCase,
            GetAllVetClinicUseCase getAllUseCase,
            GetByIdVetClinicUseCase getByIdUseCase,
            UpdateVetClinicUseCase updateUseCase,
            DeleteVetClinicUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVetClinicDto dto)
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
        public async Task<IActionResult> Update([FromBody] UpdateVetClinicDto dto)
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