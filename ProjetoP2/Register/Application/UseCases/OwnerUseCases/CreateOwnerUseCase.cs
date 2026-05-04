using ProjetoP2.Register.Application.DTOs.Owner;
using ProjetoP2.Register.Domain.Entities;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Infrastructure.Repositories;
using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class CreateOwnerUseCase
    {
        private readonly IOwnerRepository _owerRepository;

        public CreateOwnerUseCase(IOwnerRepository owerRepository)
        {
            this._owerRepository = owerRepository;
        }

        public ResponseOwnerDto Run(CreateOwnerDto dto)
        {
            var owner = new Owner(
                dto.FirstName, 
                dto.LastName,
                new Email(dto.Email),
                new PhoneNumber(dto.PhoneNumber),
                new CPF(dto.CPF)
            );

            _owerRepository.Create(owner);

            return new ResponseOwnerDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email.Value
            };
        }
    }
}
