namespace ProjetoP2.Register.Application.DTOs.Pet
{
    public class ResponsePetRegisterDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Specie { get; set; }

        public string Sex { get; set; }

        public Guid? OwnerId { get; set; }
    }
}
