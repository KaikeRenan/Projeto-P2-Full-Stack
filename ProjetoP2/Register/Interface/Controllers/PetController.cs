using Microsoft.AspNetCore.Mvc;
using ProjetoP2.Register.Application.DTOs.Pet;
using ProjetoP2.Register.Application.UseCases.PetUseCases;

namespace ProjetoP2.Register.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly CreatePetUseCase _createUseCase;
        private readonly GetPetUseCase _getUseCase;
        private readonly DeletePetUseCase _deleteUseCase;

        public PetController(CreatePetUseCase createUseCase, GetPetUseCase getUseCase, DeletePetUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public IActionResult Create(CreatePetDto dto) 
        {
            var result = _createUseCase.Run(dto);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            return Ok(_getUseCase.Run());
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid id)
        {
            _deleteUseCase.Run(id);
            return NoContent();
        }
    }
}
