namespace ProjetoP2.Clinic.Application.DTOs.Vet
{
    public class UpdateVetClinicDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CPF { get; set; }
        public string CRMV { get; set; }
    }
}
