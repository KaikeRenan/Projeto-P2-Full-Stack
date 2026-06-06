namespace ProjetoP2.Shared.Exceptions
{
    public class ExceptionDuplicateEmail : BaseException
    {
        public ExceptionDuplicateEmail() : base("Email já cadastrado") { }
    }
}
