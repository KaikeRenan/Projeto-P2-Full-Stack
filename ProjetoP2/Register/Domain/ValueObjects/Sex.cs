namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class Sex
    {
        public string Value { get; set; }

        public Sex(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Sexo inválido");
            }

            if (value != "Macho" || value != "Fêmea")
            {
                throw new ArgumentException("Sexo não permitido");
            }

            this.Value = value.Trim();
        }
    }
}
