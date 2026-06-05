using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.Exceptions;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class UpdateOwnerRegisterUseCase
    {
        private readonly IOwnerRegisterRepository _owerRepository;

        public UpdateOwnerRegisterUseCase(IOwnerRegisterRepository owerRepository)
        {
            this._owerRepository = owerRepository;
        }

        public async Task<ResponseOwnerRegisterDto> Run(UpdateOwnerRegisterDto dto)
        {
            var owner = await _owerRepository.GetByIdAsync(dto.Id);

            if (owner == null)
                throw new Exception("Owner não encontrado");

            if (owner.Email.Value != dto.Email && await _owerRepository.ExistsByEmailAsync(dto.Email))
                throw new ExceptionDuplicateEmail();

            owner.Update(
                dto.FirstName,
                dto.LastName,
                new Email(dto.Email),
                new PhoneNumber(dto.PhoneNumber)
            );

            await _owerRepository.UpdateAsync(owner);

            return new ResponseOwnerRegisterDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value
            };
        }
    }
}
