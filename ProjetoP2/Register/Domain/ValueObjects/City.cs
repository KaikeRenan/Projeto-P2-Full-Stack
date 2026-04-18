namespace ProjetoP2.Register.Domain.ValueObjects
{
    public class City
    {
        public string Value { get; set; }

        public City(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Cidade com formato inválido");
            }

            this.Value = value.Trim();
        }
    }
}
