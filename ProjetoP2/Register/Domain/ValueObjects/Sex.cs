namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class Sex
    {
        public string Value { get; private set; }

        public Sex(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Sexo inválido");

            var normalized = value.Trim().ToUpper()
                .Replace("Ê", "E")
                .Replace("ê", "E");

            if (normalized != "MACHO" && normalized != "FEMEA")
                throw new ArgumentException("Sexo não permitido");

            Value = value.Trim();
        }
    }
}