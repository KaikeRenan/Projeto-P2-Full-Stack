using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.OwnerUseCases
{
    public class DeleteOwnerUseCase
    {
        private readonly IOwnerRepository _owerRepository;

        public DeleteOwnerUseCase(IOwnerRepository owerRepository)
        {
            _owerRepository = owerRepository;
        }

        public void Run(Guid Id) 
        {
            var owner = _owerRepository.GetById(Id);

            if (owner == null)
                throw new Exception("Dono não foi encontrado");

            _owerRepository.Delete(owner);
        }
    }
}
