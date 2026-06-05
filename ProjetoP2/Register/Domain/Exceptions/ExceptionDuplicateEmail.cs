using ProjetoP2.Shared.Exceptions;

namespace ProjetoP2.Register.Domain.Exceptions
{
    public class ExceptionDuplicateEmail : BaseException
    {
        public ExceptionDuplicateEmail() : base("email já cadastrado") { }
    }
}
