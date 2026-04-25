namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class City
    {
        public string Value { get; private set; }

        public City(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Nome de cidade com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
