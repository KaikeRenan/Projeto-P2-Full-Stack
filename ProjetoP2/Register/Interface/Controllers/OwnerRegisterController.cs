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
        private readonly GetOwnerRegisterUseCase _getUseCase;
        private readonly DeleteOwnerRegisterUseCase _deleteUseCase;

        public OwnerRegisterController(
            CreateOwnerRegisterUseCase createUseCase, 
            GetOwnerRegisterUseCase getUseCase, 
            DeleteOwnerRegisterUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public IActionResult Create(CreateOwnerRegisterDto dto)
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
