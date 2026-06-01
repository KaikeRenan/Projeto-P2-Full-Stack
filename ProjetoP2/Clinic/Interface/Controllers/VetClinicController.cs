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
        private readonly GetVetClinicUseCase _getUseCase;
        private readonly DeleteVetClinicUseCase _deleteUseCase;

        public VetClinicController(
            CreateVetClinicUseCase createUseCase,
            GetVetClinicUseCase getUseCase,
            DeleteVetClinicUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getUseCase = getUseCase;
            _deleteUseCase = deleteUseCase;
        }

        [HttpPost]
        public IActionResult Create(CreateVetClinicDto dto)
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
