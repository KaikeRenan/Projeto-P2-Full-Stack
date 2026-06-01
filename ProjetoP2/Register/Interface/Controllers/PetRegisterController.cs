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
        private readonly GetPetRegisterUseCase _getUseCase;
        private readonly DeletePetRegisterUseCase _deleteUseCase;

        public PetRegisterController(
            CreatePetRegisterUseCase createUseCase, 
            GetPetRegisterUseCase getUseCase, 
            DeletePetRegisterUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public IActionResult Create(CreatePetRegisterDto dto) 
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
