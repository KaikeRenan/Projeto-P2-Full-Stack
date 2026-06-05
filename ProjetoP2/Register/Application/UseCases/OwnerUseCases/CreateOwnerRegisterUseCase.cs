using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.Exceptions;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class CreateOwnerRegisterUseCase
    {
        private readonly IOwnerRegisterRepository _owerRepository;

        public CreateOwnerRegisterUseCase(IOwnerRegisterRepository owerRepository)
        {
            this._owerRepository = owerRepository;
        }

        public async Task<ResponseOwnerRegisterDto> Run(CreateOwnerRegisterDto dto)
        {
            if (await _owerRepository.ExistsByCpfAsync(dto.CPF))
                throw new ExceptionDuplicateCpf();

            if (await _owerRepository.ExistsByEmailAsync(dto.Email))
                throw new ExceptionDuplicateEmail();

            var owner = new OwnerRegister(
                dto.FirstName, 
                dto.LastName,
                new Email(dto.Email),
                new PhoneNumber(dto.PhoneNumber),
                new CPF(dto.CPF)
            );

            await _owerRepository.CreateAsync(owner);

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
