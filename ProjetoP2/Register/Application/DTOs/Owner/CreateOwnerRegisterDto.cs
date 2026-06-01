using ProjetoP2.Shared.ValueObjects;

namespace ProjetoP2.Register.Application.DTOs.Owner
{
    public class CreateOwnerRegisterDto
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CPF { get; private set; }
    }
}
