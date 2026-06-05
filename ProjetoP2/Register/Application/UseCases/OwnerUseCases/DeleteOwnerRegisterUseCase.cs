using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class DeleteOwnerRegisterUseCase
    {
        private readonly IOwnerRegisterRepository _owerRepository;

        public DeleteOwnerRegisterUseCase(IOwnerRegisterRepository owerRepository)
        {
            _owerRepository = owerRepository;
        }

        public async Task Run(Guid Id) 
        {
            var owner = await _owerRepository.GetByIdAsync(Id);

            if (owner == null)
                throw new Exception("Dono não foi encontrado");

            await _owerRepository.DeleteAsync(owner);
        }
    }
}
