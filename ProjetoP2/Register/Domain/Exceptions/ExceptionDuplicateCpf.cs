using ProjetoP2.Shared.Exceptions;

namespace ProjetoP2.Register.Domain.Exceptions
{
    public sealed class ExceptionDuplicateCpf : BaseException
    {
        public ExceptionDuplicateCpf() : base("CPF já cadastrado") { }
    }
}
