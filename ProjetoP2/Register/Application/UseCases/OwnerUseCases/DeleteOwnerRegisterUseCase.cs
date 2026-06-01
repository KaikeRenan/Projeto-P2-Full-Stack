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

        public void Run(Guid Id) 
        {
            var owner = _owerRepository.GetById(Id);

            if (owner == null)
                throw new Exception("Dono não foi encontrado");

            _owerRepository.Delete(owner);
        }
    }
}
