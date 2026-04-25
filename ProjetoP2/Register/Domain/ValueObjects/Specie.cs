namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class Specie
    {
        public string Value { get; private set; }

        public Specie(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Espécie com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
