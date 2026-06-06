namespace ProjetoP2.Shared.Exceptions
{
    public sealed class ExceptionDuplicateCpf : BaseException
    {
        public ExceptionDuplicateCpf() : base("CPF já cadastrado") { }
    }
}
